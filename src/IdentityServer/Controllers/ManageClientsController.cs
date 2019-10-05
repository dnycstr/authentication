using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Infrastructure.Extensions;
using IdentityServer.Infrastructure.ViewModels;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{    
    //[SecurityHeaders]
    public class ManageClientsController : Controller
    {
        private ConfigurationDbContext _dbcontext;

        public ManageClientsController(ConfigurationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult GetClientsData()
        {

            var search = Request.Form["search[value]"];
            string draw = Request.Form["draw"];
            string order = Request.Form["order[0][column]"];
            string orderDir = Request.Form["order[0][dir]"];
            int startRec = Convert.ToInt32(Request.Form["start"]);
            int pageSize = Convert.ToInt32(Request.Form["length"]);

            var data = _dbcontext.Clients.ToList();

            var totalCount = data.Count();

            // Filtering process code

            var filteredCount = data.Count();

            data = data.Skip(startRec).Take(pageSize).ToList();
                        
            var tdata = new {
                draw=Convert.ToInt32(draw),
                recordsTotal = totalCount,
                recordsFiltered = filteredCount,
                data = data,
            };

            return Json(tdata);
        }

        [HttpGet]
        public IActionResult AddClient()
        {
            var model = new ClientAddViewModel();

            if (Request.IsAjaxRequest())
            {
                return PartialView("AddClient", model);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddClient(ClientAddViewModel model)
        {

            ViewBag.HttpMethod = Request.Method;

            if (!ModelState.IsValid)
            {
                model.IsSuccess = false;
                model.ErrorMessage = "Invalid model state";

                if (Request.IsAjaxRequest())
                {
                    return PartialView("AddClient", model);
                }

                return View(model);
            }

            try
            {
                var entity = new Client {
                    AuthorizationCodeLifetime = model.AuthorizationCodeLifetime,
                    ConsentLifetime = model.ConsentLifetime,
                    AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime,
                    SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime,
                    RefreshTokenUsage = model.RefreshTokenUsage,
                    UpdateAccessTokenClaimsOnRefresh = model.UpdateAccessTokenClaimsOnRefresh,
                    RefreshTokenExpiration = model.RefreshTokenExpiration,
                    AccessTokenType = model.AccessTokenType,
                    EnableLocalLogin = model.EnableLocalLogin,
                    AccessTokenLifetime = model.AccessTokenLifetime,
                    IncludeJwtId = model.IncludeJwtId,
                    AlwaysSendClientClaims = model.AlwaysSendClientClaims,
                    ClientClaimsPrefix = model.ClientClaimsPrefix,
                    PairWiseSubjectSalt = model.PairWiseSubjectSalt,
                    Created = DateTime.Now,
                    LastAccessed = DateTime.Now,
                    UserSsoLifetime = model.UserSsoLifetime,
                    UserCodeType = model.UserCodeType,
                    IdentityTokenLifetime = model.IdentityTokenLifetime,
                    AllowOfflineAccess = model.AllowOfflineAccess,
                    Enabled = model.Enabled,
                    ClientId = model.ClientId,
                    ProtocolType = model.ProtocolType,
                    RequireClientSecret = model.RequireClientSecret,
                    ClientName = model.ClientName,
                    Description = model.Description,
                    ClientUri = model.ClientUri,
                    LogoUri = model.LogoUri,
                    RequireConsent = model.RequireConsent,
                    AllowRememberConsent = model.AllowRememberConsent,
                    AlwaysIncludeUserClaimsInIdToken = model.AlwaysIncludeUserClaimsInIdToken,
                    RequirePkce = model.RequirePkce,
                    AllowPlainTextPkce = model.AllowPlainTextPkce,
                    AllowAccessTokensViaBrowser = model.AllowAccessTokensViaBrowser,
                    FrontChannelLogoutUri = model.FrontChannelLogoutUri,
                    FrontChannelLogoutSessionRequired = model.FrontChannelLogoutSessionRequired,
                    BackChannelLogoutUri = model.BackChannelLogoutUri,
                    BackChannelLogoutSessionRequired = model.BackChannelLogoutSessionRequired,
                    DeviceCodeLifetime = model.DeviceCodeLifetime,
                    NonEditable = model.NonEditable
                };

                _dbcontext.Clients.Add(entity);
                _dbcontext.SaveChanges();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_SuccessAddPartial");
                }

                return RedirectToAction("Index", "ManageClients");

            }
            catch (Exception ex)
            {

                model.IsSuccess = false;
                model.ErrorMessage = ex.Message;

                if (Request.IsAjaxRequest())
                {                    
                    return PartialView("AddClient", model);
                }

                return View(model);
            }          

        }
          
        
        public IActionResult ViewClients()
        {
            return View();
        }



    }
}
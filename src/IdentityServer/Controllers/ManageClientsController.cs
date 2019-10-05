using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Extensions;
using IdentityServer.Filters;
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

            var clients = _dbcontext.Clients.ToList();

            ViewBag.Clients = clients;

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

            data = data.Skip(startRec).Take(pageSize).ToList();

            var filteredCount = data.Count();

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
            var model = new Client();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddClient(Client client)
        {
            
            if (!ModelState.IsValid)
            {
                return View(client);
            }

            //_dbcontext.Clients.Add(client);
            //_dbcontext.SaveChanges();

            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","ManageClients");
            }

        }
    }
}
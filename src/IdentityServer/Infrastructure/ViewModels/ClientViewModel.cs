
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Infrastructure.ViewModels
{
    public class ClientViewModel : Client
    {
        
    }

    public class ClientAddViewModel : ClientViewModel
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class ClientEditViewModel : ClientViewModel
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}

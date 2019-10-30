using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamebaseApi.Auth;
using Ipstset.Core;
using Microsoft.AspNetCore.Mvc;

namespace GamebaseApi
{
    public class BaseController : Controller
    {
        public AppUser AppUser => ClaimsService.CreateAppUser(User.Claims);
    }
}

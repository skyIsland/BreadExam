using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Bread.ExamSystem.Project.Model;
using Bread.ExamSystem.Project.Model.Dto;
using Bread.ExamSystem.Project.ViewModel.HomeVMs;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Auth;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;

namespace Bread.ExamSystem.Project.Areas.API.Controllers
{
    [Area("API")]
    [AllowAnonymous]
    [ActionDescription("试卷组装")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationApiController : BaseApiController
    {

        private readonly IConfiguration _configuration;
        public AuthenticationApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("~/ExternalLogin")]
        public IActionResult ExternalLogin(string provider, string terminal)
        {
            HttpContext.Session.SetString("terminal", terminal);
            switch (provider)
            {
                case ("github"):
                    return Redirect("https://github.com/login/oauth/authorize?client_id=d08c83abe14c9f4e20f8");
                default:
                    break;
            }
            return Content("参数错误");

        }       
    }
}

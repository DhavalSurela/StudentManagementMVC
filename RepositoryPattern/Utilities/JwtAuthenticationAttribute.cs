using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using RepositoryPattern.Services;
using RepositoryPattern.Core.Models;
using RepositoryPattern.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Controllers;


namespace RepositoryPattern.Utilities
{
    public class JwtAuthenticationAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
          
            var request = filterContext.HttpContext.Request;
            var token = request.Cookies["jwt"];
            var jwtkey = request.Cookies["jwtkey"];

            if (token != null && jwtkey!= null)
            {
                var userName = Authentication.ValidateToken(token,jwtkey);
                if (userName == null)
                {
                    //filterContext.ModelState.AddModelError(nameof(Login.UserName), "UserName can't be empty");
                    filterContext.HttpContext.Response.Redirect("Login");
                }
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("Login");

            }

            base.OnActionExecuting(filterContext);
        }
    }
}
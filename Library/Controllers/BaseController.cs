﻿namespace Library.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Authorize]
    public class BaseController : Controller
    {
        protected string GetUserID()
        {
            string id = string.Empty;
            if(User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return id;
        }
    }
}

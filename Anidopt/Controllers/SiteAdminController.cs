﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anidopt.Controllers;

[Authorize(Roles = "SiteAdmin")]
public class SiteAdminController : Controller {
    public IActionResult AdminPanel() {
        return View();
    }
}

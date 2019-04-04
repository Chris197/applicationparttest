using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Module
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : Controller
    {
        public ModuleController()
        {

        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> GetFromModule()
        {
            return "result from module";
        }

    }
}

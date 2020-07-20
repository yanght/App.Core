using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Api.Controllers.v2
{

    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "this is v2";
        }
        [HttpGet]
        public string Get1()
        {
            return "this is v2";
        }
    }
}

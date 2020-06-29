using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Application.Contracts.LinCms;
using App.Core.FreeSql.DbContext;
using App.Core.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IFreeSql<AdminContext> _adminsql;
        private IFreeSql<LinCmsContext> _linecmssql;
        private IBookService _bookService;
        public ValuesController(IFreeSql<AdminContext> adminsql, IFreeSql<LinCmsContext> linecmssql, IBookService bookService)
        {
            _adminsql = adminsql; _linecmssql = linecmssql; _bookService = bookService;
        }

        [HttpGet]
        public void Get()
        {
            var i = _adminsql.Ado.ExecuteScalar("select count(1) from ad_api");
            var j = _linecmssql.Ado.ExecuteScalar("select count(1) from lin_user");
            var k = _bookService.GetBooks();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Application.Contracts.LinCms;
using App.Core.Data.Enums;
using App.Core.Data.Output;
using App.Core.Exceptions;
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
        private IAdApiRepository _adapiService;
        public ValuesController(IFreeSql<AdminContext> adminsql, IFreeSql<LinCmsContext> linecmssql, IBookService bookService, IAdApiRepository adapiService)
        {
            _adminsql = adminsql; _linecmssql = linecmssql; _bookService = bookService;
            _adapiService = adapiService;
        }

        [HttpGet]
        public async Task<IResponseOutput> Get()
        {
            //var i = _adminsql.Ado.ExecuteScalar("select count(1) from ad_api");
            //var j = _linecmssql.Ado.ExecuteScalar("select count(1) from lin_user");
            var result = await _bookService.GetBooks();
            //throw new AppException("测试错误");
            return result;
            //await _adapiService.TranTest();

        }
    }
}

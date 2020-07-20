using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Application.Contracts.Admin.AdDocument;
using App.Core.Application.Contracts.LinCms;
using App.Core.Data.Output;
using App.Core.FreeSql.DbContext;
using App.Core.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IFreeSql<AdminContext> _adminsql;
        private IFreeSql<LinCmsContext> _linecmssql;
        private IBookService _bookService;
        private IAdApiRepository _adapiService;
        private IDocumentService _documentService;
        public ValuesController(IFreeSql<AdminContext> adminsql,
            IFreeSql<LinCmsContext> linecmssql,
            IBookService bookService,
            IAdApiRepository adapiService,
            IDocumentService documentService)
        {
            _adminsql = adminsql; _linecmssql = linecmssql; _bookService = bookService;
            _adapiService = adapiService;
            _documentService = documentService;
        }

        [HttpGet]
        public string Get()
        {
            return "this is v1";
        }

        [HttpPost]
        public async Task<IResponseOutput> AddAndUpdateDocument()
        {
            //var i = _adminsql.Ado.ExecuteScalar("select count(1) from ad_api");
            //var j = _linecmssql.Ado.ExecuteScalar("select count(1) from lin_user");
            //var result = await _bookService.GetBooks();
            //throw new AppException("测试错误");
            var result = await _documentService.AddAndUpdateDocument();
            return result;
            //await _adapiService.TranTest();

        }

        [HttpPost]
        public async Task<IResponseOutput> AddAndUpdateBooks()
        {
            var result = await _bookService.AddAndUpdateBooks();
            return result;
        }
    }
}

using App.Core.Aop.Attributes;
using App.Core.Application.Contracts.LinCms;
using App.Core.FreeSql.DbContext;
using App.Core.FreeSql.UseUnitOfWork;
using App.Core.IRepositories;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.LinCms
{
    public class BookService : IBookService
    {
        private IBookRepository _repo;
        private static IUnitOfWork<LinCmsContext> _uwo;
        public BookService(IBookRepository repo, IUnitOfWork<LinCmsContext> uwo)
        {
            _repo = repo;
            _uwo = uwo;
        }
        [Transactional]
        public async Task GetBooks()
        {
            await _repo.GetBooks();
            //await _uowManager.CommitAsync();
        }
    }
}

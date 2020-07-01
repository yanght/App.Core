using App.Core.Aop.Attributes;
using App.Core.Application.Contracts.LinCms;
using App.Core.Application.Contracts.LinCms.Books.Output;
using App.Core.Data.Enums;
using App.Core.Data.Output;
using App.Core.Entitys.LinCms;
using App.Core.FreeSql.DbContext;
using App.Core.FreeSql.UseUnitOfWork;
using App.Core.IRepositories;
using AutoMapper;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.LinCms
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private IBookRepository _repo;
        private static IUnitOfWork<LinCmsContext> _uow;
        public BookService(IBookRepository repo, IUnitOfWork<LinCmsContext> uow, IMapper mapper)
        {
            _repo = repo;
            _uow = uow;
            _mapper = mapper;
        }
        [Transactional]
        public async Task<IResponseOutput<BookGetOutput>> GetBooks()
        {
            var book = await _repo.GetBooks();
            var response = _mapper.Map<BookGetOutput>(book);
            return new ResponseOutput<BookGetOutput>().Ok(response);
        }
    }
}

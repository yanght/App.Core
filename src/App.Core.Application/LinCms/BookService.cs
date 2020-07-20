using App.Core.Aop.Attributes;
using App.Core.Application.Contracts.Amdin.AdDocument.Output;
using App.Core.Application.Contracts.LinCms;
using App.Core.Application.Contracts.LinCms.Books.Output;
using App.Core.Data.Output;
using App.Core.FreeSql.DbContext;
using App.Core.FreeSql.UseUnitOfWork;
using App.Core.IRepositories;
using AutoMapper;
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
        public async Task<IResponseOutput<BookGetOutput>> AddAndUpdateBooks()
        {
            var book = await _repo.AddAndUpdateBooks();
            var response = _mapper.Map<BookGetOutput>(book);
            return new ResponseOutput<BookGetOutput>().NotOk(Data.Enums.ErrorCode.Error,"凑五信息");
        }


    }
}

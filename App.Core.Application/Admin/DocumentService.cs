using App.Core.Aop.Attributes;
using App.Core.Application.Contracts.Admin.AdDocument;
using App.Core.Application.Contracts.Amdin.AdDocument.Output;
using App.Core.Application.Contracts.LinCms.Books.Output;
using App.Core.Data.Output;
using App.Core.Exceptions;
using App.Core.IRepositories.Admin;
using AutoMapper;
using FreeSql;
using System;
using System.Threading.Tasks;

namespace App.Core.Application.Contracts.Admin
{
    public class DocumentService : IDocumentService
    {
        private readonly IMapper _mapper;
        private IDocumentRepository _repo;
        public DocumentService(IDocumentRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [Transactional]
        public async Task<IResponseOutput<DocumentGetOutput>> AddAndUpdateDocument()
        {
            //using (_repo.UnitOfWork.GetOrBeginTransaction())
            //{
                var model = await _repo.InsertAsync(new Entitys.Admin.AdDocumentModel()
                {
                    Content = "tets",
                    CreatedTime = DateTime.Now,
                    CreatedUserId = 0,
                    CreatedUserName = "yannis",
                    Description = "desc",
                    Enabled = true,
                    Html = "htmnl",
                    Type = 1,
                    ParentId = 0,
                    Label = new Random().Next().ToString(),
                });
                model = await _repo.GetAsync(model.Id);
                model.Label = "1";
                throw new AppException("事务异常");
                await _repo.UpdateAsync(model);
                var result = _mapper.Map<DocumentGetOutput>(model);
                return new ResponseOutput<DocumentGetOutput>().Ok(result);
            //}

        }
    }
}

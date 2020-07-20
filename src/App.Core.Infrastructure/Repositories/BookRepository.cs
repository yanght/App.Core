using App.Core.Entitys.Admin;
using App.Core.Entitys.LinCms;
using App.Core.FreeSql.DbContext;
using App.Core.FreeSql.Repositories;
using App.Core.FreeSql.UseUnitOfWork;
using App.Core.IRepositories;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Infrastructure.Repositories
{
    public class BookRepository : RepositoryBase<BookModel, LinCmsContext>, IBookRepository
    {
        public BookRepository(IUnitOfWork<LinCmsContext> unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<BookModel> AddAndUpdateBooks()
        {
            //using (var tran = UnitOfWork.GetOrBeginTransaction())
            //{
            
            var book = new BookModel()
            {
                Author = "test",
                CreateTime = DateTime.Now,
                CreateUserId = 0,
                DeleteTime = DateTime.Now,
                DeleteUserId = 0,
                Image = "http://www.baidu.com",
                IsDeleted = false,
                Summary = "test",
                Title = "title",
                UpdateTime = DateTime.Now,
                UpdateUserId = 0
            };
            var model = await InsertAsync(book);
            model.Title = "更新title";
            await UpdateAsync(model);
            
            return Select.WhereDynamic(1).ToOne();
            //    tran.Commit();
            //}
        }
    }
}

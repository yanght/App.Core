using App.Core.Entitys.Admin;
using App.Core.Entitys.LinCms;
using App.Core.FreeSql.DbContext;
using App.Core.FreeSql.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.IRepositories
{
    public interface IBookRepository : IRepositoryBase<BookModel, LinCmsContext>
    {
        Task<BookModel> AddAndUpdateBooks();
    }
}

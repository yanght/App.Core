using App.Core.Entitys.Admin;
using App.Core.Entitys.LinCms;
using App.Core.FreeSql.DbContext;
using App.Core.IRepositories;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Infrastructure.Repositories
{
    public class BookRepository : RepositoryBase<BookModel>, IBookRepository
    {
        IFreeSql<AdminContext> _adminsql;
        public BookRepository(IFreeSql<LinCmsContext> fsql, IFreeSql<AdminContext> adminsql) : base(fsql)
        {
            _adminsql = adminsql;
        }

        public object GetBooks()
        {
            var repo = _adminsql.GetRepository<AdApiModel>();
            repo.Select.Count();
            Select.Count();
            return 0;
        }
    }
}

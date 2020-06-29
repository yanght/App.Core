using App.Core.Entitys.Admin;
using App.Core.Entitys.LinCms;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.IRepositories
{
    public interface IBookRepository : IRepositoryBase<BookModel>
    {
        object GetBooks();
    }
}

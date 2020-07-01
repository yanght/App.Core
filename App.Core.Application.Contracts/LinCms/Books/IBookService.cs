using App.Core.Application.Contracts.LinCms.Books.Output;
using App.Core.Data.Output;
using App.Core.Entitys.LinCms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Util.Dependency;

namespace App.Core.Application.Contracts.LinCms
{
    public interface IBookService : IScopeDependency
    {
        Task<IResponseOutput<BookGetOutput>> GetBooks();
    }
}

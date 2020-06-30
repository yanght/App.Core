using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Util.Dependency;

namespace App.Core.Application.Contracts.LinCms
{
    public interface IBookService : IScopeDependency
    {
        Task GetBooks();
    }
}

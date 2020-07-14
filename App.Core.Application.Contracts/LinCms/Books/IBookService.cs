using App.Core.Application.Contracts.Amdin.AdDocument.Output;
using App.Core.Data.Output;
using System.Threading.Tasks;
using Util.Dependency;

namespace App.Core.Application.Contracts.LinCms
{
    public interface IBookService : IScopeDependency
    {
        Task<IResponseOutput<DocumentGetOutput>> AddAndUpdateBooks();
    }
}

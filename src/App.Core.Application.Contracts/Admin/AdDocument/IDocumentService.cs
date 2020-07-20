using App.Core.Application.Contracts.Amdin.AdDocument.Output;
using App.Core.Data.Output;
using System.Threading.Tasks;

namespace App.Core.Application.Contracts.Admin.AdDocument
{
    public interface IDocumentService
    {
        Task<IResponseOutput<DocumentGetOutput>> AddAndUpdateDocument();
    }
}

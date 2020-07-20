using App.Core.Entitys.Admin;
using App.Core.FreeSql.Repositories;
using App.Core.IRepositories.Admin;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Infrastructure.Repositories.Admin
{
    public class DocumentRepository : SimpleRepositoryBase<AdDocumentModel>, IDocumentRepository
    {
        public DocumentRepository(UnitOfWorkManager uowm) : base(uowm)
        {

        }
    }
}

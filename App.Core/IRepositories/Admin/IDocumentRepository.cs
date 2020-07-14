using App.Core.Entitys.Admin;
using App.Core.FreeSql.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace App.Core.IRepositories.Admin
{
    public interface IDocumentRepository : ISimpleRepositoryBase<AdDocumentModel>
    {
    }
}

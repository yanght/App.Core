using App.Core.Entitys.Admin;
using App.Core.FreeSql.DbContext;
using App.Core.FreeSql.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.IRepositories
{
    public interface IAdApiRepository : IRepositoryBase<AdApiModel, AdminContext>
    {
        Task TranTest();
    }
}

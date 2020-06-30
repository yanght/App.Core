using App.Core.Entitys.Admin;
using App.Core.FreeSql.DbContext;
using App.Core.FreeSql.Repositories;
using App.Core.FreeSql.UseUnitOfWork;
using App.Core.IRepositories;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Util.Datas.Queries;

namespace App.Core.Infrastructure.Repositories
{
    public class AdApiRepository : RepositoryBase<AdApiModel, AdminContext>, IAdApiRepository
    {
        public AdApiRepository(IUnitOfWork<AdminContext> unitOfWork) : base(unitOfWork)
        {

        }
        public async Task TranTest()
        {
            //using (var tran = UnitOfWork.GetOrBeginTransaction())
            //{
                var model = await Select.Where(m => m.Id == 1).FirstAsync();

                model.Name = "测试修改";

                await UpdateAsync(model);

            //    tran.Commit();

            //}

        }
    }
}

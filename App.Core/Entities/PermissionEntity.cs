using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities
{
    [Table(Name = "permissions")]
    public class PermissionEntity : FullAduitEntity<long>
    {
        public PermissionEntity(string name, string module)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Module = module ?? throw new ArgumentNullException(nameof(module));
        }

        public PermissionEntity()
        {
        }

        /// <summary>
        /// 所属权限、权限名称，例如：访问首页
        /// </summary>
        [Column(DbType = "varchar(60)")]
        public string Name { get; set; }

        /// <summary>
        /// 权限所属模块，例如：人员管理
        /// </summary>
        [Column(DbType = "varchar(50)")]
        public string Module { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" Auth:{Name}、Module:{Module}";
        }
    }
}

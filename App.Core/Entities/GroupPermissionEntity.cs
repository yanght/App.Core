using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities
{
    [Table(Name = "group_permission")]
    public class GroupPermissionEntity : Entity<long>
    {
        public GroupPermissionEntity()
        {
        }

        public GroupPermissionEntity(long groupId, long permissionId)
        {
            GroupId = groupId;
            PermissionId = permissionId;
        }

        public GroupPermissionEntity(long permissionId)
        {
            PermissionId = permissionId;
        }

        /// <summary>
        /// 分组id
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        public long PermissionId { get; set; }


    }
}

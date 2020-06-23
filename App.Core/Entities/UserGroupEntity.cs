using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
    [Table(Name = "user_group")]
    public class UserGroupEntity : Entity<long>
    {
        public UserGroupEntity()
        {
        }
        public UserGroupEntity(long userId, long groupId)
        {
            UserId = userId;
            GroupId = groupId;
        }

        public long UserId { get; set; }

        public long GroupId { get; set; }

        [Navigate("UserId")]
        public UserEntity User { get; set; }

        [Navigate("GroupId")]
        public GroupEntity Group { get; set; }
    }
}

using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Group : BaseEntity
    {
        public string GroupName { get; set; }
        public virtual ICollection<GroupClaim> GroupClaims { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
using System.Collections.Generic;
using Core.Entities;

namespace Entities.Concrete
{
    public class OperationClaim : BaseEntity
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public virtual ICollection<GroupClaim> GroupClaims { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
    }
}
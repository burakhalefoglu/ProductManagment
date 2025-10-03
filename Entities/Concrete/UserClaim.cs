using Core.Entities;

namespace Entities.Concrete
{
    public class UserClaim : IEntity
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
        public virtual OperationClaim Claim { get; set; }
        public virtual User User { get; set; }
    }
}
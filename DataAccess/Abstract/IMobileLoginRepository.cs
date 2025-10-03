using Core.DataAccess;
using Entities.Concrete;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IMobileLoginRepository : IEntityRepository<MobileLogin>
    {
    }
}
using Core.Entities;

namespace Entities.Concrete
{
    public class Color: IEntity

    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
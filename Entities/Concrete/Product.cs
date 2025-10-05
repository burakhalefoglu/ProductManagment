using System.Collections.Generic;
using Core.Entities;

namespace Entities.Concrete
{
    public class Product: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public ICollection<Color> Colors { get; set; } = new List<Color>();
    }
    
}
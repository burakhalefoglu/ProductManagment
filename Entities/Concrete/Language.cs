using System.Collections.Generic;
using Core.Entities;

namespace Entities.Concrete
{
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Translate> Translates { get; set; }

    }
}
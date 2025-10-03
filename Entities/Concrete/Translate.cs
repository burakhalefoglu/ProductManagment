using Core.Entities;

namespace Entities.Concrete
{
    public class Translate : BaseEntity
    {
        public int LangId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public virtual Language Languages { get; set; }

    }
}
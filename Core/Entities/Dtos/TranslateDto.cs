namespace Core.Entities.Dtos
{
    public class TranslateDto : IDto
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public int LanguageId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
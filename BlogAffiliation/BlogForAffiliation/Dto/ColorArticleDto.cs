namespace Sxylo_Stock.Dto
{
    public class ColorArticleDto
    {
        public string libelle { get; set; }
        public string description { get; set; }
    }

    public class UpdateColorArticleDto
    {
        public int id { get; set; }
        public string libelle { get; set; }
        public string description { get; set; }
    }
}

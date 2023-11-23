namespace Sxylo_Stock.Dto
{
    public class SizeArticleDto
    {
        public string libelle { get; set; }
        public int subCategoryid { get; set; }
    }

    public class UpdateSizeArticleDto
    {
        public int id { get; set; }
        public string libelle { get; set; }
        public int subCategoryid { get; set; }
    }
}

using Sxylo_Stock.Model;

namespace Sxylo_Stock.Dto
{
    public class ArticleDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public string lienAffiliation { get; set; }
        public int oldPrice { get; set; }
        public int newPrice { get; set; }
        public int quantity { get; set; }
        public int alertquantity { get; set; }
        public DateTime dateExpiration { get; set; }
        public int nbreMonthGaranti { get; set; }
        public string numSeri { get; set; }
        public string memoryStorage { get; set; }
        public string modelName { get; set; }
        public int renewed { get; set; }
        public string operatingSystem { get; set; }
        public IFormFile pictureArticle { get; set; }
        public int sizeid { get; set; }
        public string colorid { get; set; }
        public int subCategoryid { get; set; }
    }

    public class MoreImageDto
    {
        public List<IFormFile> listImageOfArticle { get; set; }
        public int IdArticle { get; set; }
    }
}

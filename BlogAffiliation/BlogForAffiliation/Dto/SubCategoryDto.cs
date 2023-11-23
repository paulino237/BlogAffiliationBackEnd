namespace Sxylo_Stock.Dto
{
    public class SubCategoryDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public int categoryid { get; set; }
    }

    public class UpdateSubCategoryDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int categoryid { get; set; }
    }
}

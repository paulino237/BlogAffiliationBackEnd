namespace Sxylo_Stock.Dto
{
    public class PictureArticleDto
    {
        public IFormFile pathJoinced { get; set; }
        public string fileName { get; set; }
    }

    public class UpdatePictureArticleDto
    {
        public int id { get; set; }
        public IFormFile pathJoinced { get; set; }
        public string fileName { get; set; }
    }

    public class ResponseImage
    {
        public string message { get; set; }
        public int statusCode { get; set; }
        public string nameFile { get; set; }


    }
}

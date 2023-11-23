using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sxylo_Stock.Model
{
	public class PictureArticle
	{
        [Key]
        public int id { get; set; }
        public string pathJoinced { get; set; }
        public string fileName { get; set; }
        public DateTime creatAt { get; set; }
        public int archived { get; set; }
        public int articleid  { get; set; }
        public Article  article { get; set; }



    }
}


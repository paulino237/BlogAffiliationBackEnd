using System;
using System.ComponentModel.DataAnnotations;
namespace Sxylo_Stock.Model.Entities
{
	public class AssocSizeArticle
	{
		
        [Key]
        public int id { get; set; }

        public int articleid { get; set; }
        public Article article { get; set; }

        public int sizeArticleid { get; set; }
        public string libelle { get; set; }
        public DateTime creatAt { get; set; }
        public int archived { get; set; }
    
	}
}


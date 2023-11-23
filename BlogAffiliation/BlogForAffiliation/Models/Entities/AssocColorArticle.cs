using System;
using System.ComponentModel.DataAnnotations;

namespace Sxylo_Stock.Model
{
	public class AssocColorArticle
	{
        [Key]
        public int id { get; set; }

        public int articleid { get; set; }
        public Article article { get; set; }

        public int colorArticleid { get; set; }
        public ColorArticle colorArticle { get; set; }

        public string libelle { get; set; }     
        public DateTime creatAt { get; set; }
        public int archived { get; set; }
    }
}


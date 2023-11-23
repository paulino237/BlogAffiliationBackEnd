using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sxylo_Stock.Model
{
	public class SubCategory
	{
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime creatAt { get; set; }
        public int archived { get; set; } 
        public int categoryid { get; set; }
        public Category category { get; set; }

        [JsonIgnore]
        public ICollection<SizeArticle> sizeArticles { get; set; }
    }
}


using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sxylo_Stock.Model
{
	public class Category
	{
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime creatAt { get; set; }
        public int archived { get; set; }

        [JsonIgnore]
        public ICollection<SubCategory> subCategory { get; set; }
    }
}


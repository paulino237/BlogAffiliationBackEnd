using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sxylo_Stock.Model
{
	public class ColorArticle
	{
        [Key]
        public int id { get; set; }
        public string libelle { get; set; }
        public string description { get; set; }
        public DateTime creatAt { get; set; }
        public int archived { get; set; }
    }
}


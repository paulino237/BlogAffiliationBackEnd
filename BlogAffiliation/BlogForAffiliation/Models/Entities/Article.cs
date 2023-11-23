using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Sxylo_Stock.Model
{
	public class Article
	{
        [Key]
        public int id { get; set; }
        public string codeArticle { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int oldPrice { get; set; }
        public int newPrice { get; set; }
        public int quantity { get; set; }
        public string lienAffiliation { get; set; }
        public int status { get; set; }
        public int etat { get; set; }
        public DateTime creatAt { get; set; }
        public DateTime updatAt { get; set; }
        public int alertquantity { get; set; }
        public DateTime dateExpiration { get; set; }
        public int nbreMonthGaranti { get; set; }
        public int archived { get; set; }
       
        public string numSeri { get; set; }
        public string memoryStorage { get; set; }
        public string modelName { get; set; }
        public int renewed { get; set; }
        public string operatingSystem { get; set; }
       
        public int subCategoryid { get; set; }
        public SubCategory subCategory { get; set; }



    }
}


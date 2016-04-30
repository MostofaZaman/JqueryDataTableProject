using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoProjectMVC.Models
{
    [Table("Category", Schema = "INV")]
    public class Category
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhotoFileName { get; set; }
    }
    [Table("Product", Schema = "INV")]
    public class Product
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}
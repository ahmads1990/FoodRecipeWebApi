using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRecipeWebApi.Models
{
    public class Recipe : BaseModel
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; } 
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("Category")]
        public int CategroryId { get; set; }
        public Category Category { get; set; }
    }
}

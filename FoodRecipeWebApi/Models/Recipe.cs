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
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    }
}

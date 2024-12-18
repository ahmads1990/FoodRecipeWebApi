using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRecipeWebApi.Models
{
    public class Favourite : BaseModel
    {
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
        [ForeignKey("Recipe")]

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}

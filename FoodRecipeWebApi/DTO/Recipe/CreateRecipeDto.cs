using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodRecipeWebApi.DTO.Recipes
{
    public class CreateRecipeDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Tag { get; set; }
        [MinLength(2)]
        [MaxLength(50)]
        public string Description { get; set; }
        [FromForm]
        public IFormFile Image { get; set; }
        [Range(1, 1000)]
        public decimal Price { get; set; }

        public string CategroryId { get; set; }
    }
}

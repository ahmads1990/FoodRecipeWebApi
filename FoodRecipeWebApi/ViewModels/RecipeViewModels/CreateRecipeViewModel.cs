﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodRecipeWebApi.ViewModels.RecipeViewModel
{
    public class CreateRecipeViewModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        public string Tag { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        [FromForm]
        public IFormFile Image { get; set; }
        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }

        public int CategroryId { get; set; }
    }
}

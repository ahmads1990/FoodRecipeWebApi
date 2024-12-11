namespace FoodRecipeWebApi.Models;

public class BaseModel
{
    public int ID { get; set; }
    public bool Deleted { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
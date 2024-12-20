namespace FoodRecipeWebApi.ViewModels.Auth;

public record ConfirmEmailRequest(
      int UserId,
      String Code
      );

using BussinessLogic.Enums;

namespace BussinessLogic.DTOs;

public record RecipeAddDTO(
    string Title,
    string Description,
    string ImageUrl,
    int CookingTime,
    RecipeDifficulty Difficulty,
    string Body,
    List<IngredientQuantityAddDTO> Ingredients,
    string? VideoUrl = null,
    int Servings = 1
);
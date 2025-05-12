using BussinessLogic.Services.Interfaces;
using BussinessLogic.Enums;
using BussinessLogic.DTOs;
using BussinessLogic.Extensions;

namespace BussinessLogic.Services.Implementations;

public class RecipeService(HttpClient crudClient) : IRecipeService
{
    private static RecipePreviewDTO RecipePreview(RecipeDTO recipe) => new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            ImageUrl = recipe.ImageUrl,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Difficulty = recipe.Difficulty,
        };

    public async Task Add(RecipeAddDTO recipe)
    {
        await crudClient.PostAsync("/Recipe", recipe).Unpack();
    }

    public async Task<RecipeDTO> GetById(Guid id)
    {
        return await crudClient.GetAsync($"/Recipe/{id}")
            .FromJson<RecipeDTO>();
    }

    public async Task<List<RecipePreviewDTO>> GetAll()
    {
        return await crudClient.GetAsync("/Recipe")
            .FromJson<List<RecipePreviewDTO>>();
    }

    public async Task<List<RecipePreviewDTO>> Search(CleanUserDTO user, RecipeFiltersDTO filters)
    {
        var fridge = !filters.OnlyFridge ? null :
            await crudClient.GetAsync($"/User/{user.Id}/Fridge")
                .FromJson<List<FridgeIngredientDTO>>();

        var allRecipes = await crudClient.GetAsync("/Recipe?withIngredients=true")
            .FromJson<List<RecipeDTO>>();

        var recipes = allRecipes
            .Where(r => filters.MaxCookingTime == null || r.CookingTime <= filters.MaxCookingTime)
            .Where(r => filters.MaxDifficulty  == null || r.Difficulty  <= filters.MaxDifficulty)
            .Where(r => !filters.OnlyFridge || r.Ingredients.All(i => 
                fridge!.Any(f => f.IngredientId == i.IngredientId)))
            .ToList();

        recipes.FindAll(r => !filters.OnlyFridge || r.Ingredients.All(i => {
            var fridgeIngredient = fridge!.FirstOrDefault(f => f.IngredientId == i.IngredientId);
            var conversionFactor = UnitExtensions.Convert(fridgeIngredient!.Unit, i.Unit);
            return fridgeIngredient.Quantity * conversionFactor >= i.Quantity;
        }));

        return [.. recipes.Select(RecipePreview)];
    }

    public async Task<RecipeDTO> Update(Guid id, RecipeUpdateDTO recipeUpdates)
    {
        return await crudClient.PatchAsync($"/Recipe/{id}", recipeUpdates)
            .FromJson<RecipeDTO>();
    }

    public async Task Delete(Guid id)
    {
        await crudClient.DeleteAsync($"/Recipe/{id}").Unpack();
    }
}
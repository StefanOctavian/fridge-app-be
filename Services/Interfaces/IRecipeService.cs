using BussinessLogic.DTOs;

namespace BussinessLogic.Services.Interfaces;

public interface IRecipeService
{
    public Task Add(RecipeAddDTO recipe);
    public Task<RecipeDTO> GetById(Guid id);
    public Task<List<RecipePreviewDTO>> GetAll();
    public Task<List<RecipePreviewDTO>> Search(CleanUserDTO user, RecipeFiltersDTO filters);
    public Task<RecipeDTO> Update(Guid id, RecipeUpdateDTO recipeUpdates);
    public Task Delete(Guid id);
}
using BussinessLogic.DTOs;

namespace BussinessLogic.Services.Interfaces;

public interface IIngredientService
{
    public Task<List<IngredientDTO>> GetAll();
    public Task<IngredientDTO> GetById(Guid id);
    public Task<IngredientDTO> GetByName(string name);
    public Task<IngredientDTO> Add(IngredientAddDTO ingredient);
    public Task<IngredientDTO> Update(Guid id, IngredientUpdateDTO ingredient);
    public Task Delete(Guid id);
}
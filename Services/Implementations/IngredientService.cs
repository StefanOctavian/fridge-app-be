using Microsoft.EntityFrameworkCore;

using BussinessLogic.Services.Interfaces;
using BussinessLogic.DTOs;
using BussinessLogic.Errors;
using BussinessLogic.Extensions;

namespace BussinessLogic.Services.Implementations;

public class IngredientService(HttpClient httpClient) : IIngredientService
{
    public async Task<List<IngredientDTO>> GetAll()
    {
        return await httpClient.GetAsync("/Ingredient")
            .FromJson<List<IngredientDTO>>();
    }

    public async Task<IngredientDTO> GetById(Guid id)
    {
        return await httpClient.GetAsync($"/Ingredient/{id}")
            .FromJson<IngredientDTO>();
    }

    public async Task<IngredientDTO> GetByName(string name)
    {
        return await httpClient.GetAsync($"/Ingredient?name={name}")
            .FromJson<IngredientDTO>();
    }

    public async Task<IngredientDTO> Add(IngredientAddDTO ingredient)
    {
        return await httpClient.PostAsync("/Ingredient", ingredient)
            .FromJson<IngredientDTO>();
    }

    public async Task<IngredientDTO> Update(Guid id, IngredientUpdateDTO ingredient)
    {
        return await httpClient.PatchAsync($"/Ingredient/{id}", ingredient)
            .FromJson<IngredientDTO>();
    }

    public async Task Delete(Guid id)
    {
        await httpClient.DeleteAsync($"/Ingredient/{id}").Unpack();
    }
}
using BussinessLogic.DTOs;
using BussinessLogic.Services.Interfaces;
using BussinessLogic.Extensions;

namespace BussinessLogic.Services.Implementations;

public class UserService(HttpClient crudClient) : IUserService
{
    private static CleanUserDTO CleanUser(UserDTO user)
    {
        return new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
        };
    }

    public async Task<CleanUserDTO> GetUser(Guid id)
    {
        var user = await crudClient.GetAsync($"/User/{id}").FromJson<UserDTO>();
        return CleanUser(user);
    }

    public async Task<CleanUserDTO> GetUser(string email)
    {
        var user = await crudClient.GetAsync($"/User?email={email}").FromJson<UserDTO>();
        return CleanUser(user);
    }

    public async Task<CleanUserDTO> UpdateUser(Guid id, UserUpdateDTO userUpdate)
    {
        var user = await crudClient.PatchAsync($"/User/{id}", userUpdate).FromJson<UserDTO>();
        return CleanUser(user);
    }

    public async Task<List<FridgeIngredientDTO>> GetFridge(CleanUserDTO currentUser)
    {
        var fridge = await crudClient.GetAsync($"/User/{currentUser.Id}/Fridge")
            .FromJson<List<FridgeIngredientDTO>>();
        return fridge;
    }

    public async Task<List<FridgeIngredientDTO>> PutFridge(
        CleanUserDTO currentUser, List<FridgePutIngredientDTO> fridge
    )
    {
        return await crudClient.PutAsync($"/User/{currentUser.Id}/Fridge", fridge)
            .FromJson<List<FridgeIngredientDTO>>();
    }

    public async Task<List<FridgeIngredientDTO>> UpdateFridge(
        CleanUserDTO currentUser, List<FridgeIngredientDeltaDTO> fridgeDelta
    )
    {
        return await crudClient.PatchAsync($"/User/{currentUser.Id}/Fridge", fridgeDelta)
            .FromJson<List<FridgeIngredientDTO>>();
    }
}
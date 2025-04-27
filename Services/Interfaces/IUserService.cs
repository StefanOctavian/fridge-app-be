using BussinessLogic.DTOs;
using BussinessLogic.Entities;

namespace BussinessLogic.Services.Interfaces;

public interface IUserService 
{
    Task<CleanUserDTO> GetUser(Guid id);
    Task<CleanUserDTO> GetUser(string email);
    Task<List<FridgeIngredientDTO>> GetFridge(UserDTO currentUser);
    Task<CleanUserDTO> UpdateUser(Guid id, UserUpdateDTO userUpdate);
    Task<List<FridgeIngredientDTO>> PutFridge(
        UserDTO currentUser,
        List<FridgePutIngredientDTO> ingredients
    );
    Task<List<FridgeIngredientDTO>> UpdateFridge(
        UserDTO currentUser,
        List<FridgeIngredientDeltaDTO> ingredientsDelta
    );

}
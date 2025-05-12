using BussinessLogic.DTOs;

namespace BussinessLogic.Services.Interfaces;

public interface IUserService 
{
    Task<CleanUserDTO> GetUser(Guid id);
    Task<CleanUserDTO> GetUser(string email);
    Task<List<FridgeIngredientDTO>> GetFridge(CleanUserDTO currentUser);
    Task<CleanUserDTO> UpdateUser(Guid id, UserUpdateDTO userUpdate);
    Task<List<FridgeIngredientDTO>> PutFridge(
        CleanUserDTO currentUser,
        List<FridgePutIngredientDTO> ingredients
    );
    Task<List<FridgeIngredientDTO>> UpdateFridge(
        CleanUserDTO currentUser,
        List<FridgeIngredientDeltaDTO> ingredientsDelta
    );

}
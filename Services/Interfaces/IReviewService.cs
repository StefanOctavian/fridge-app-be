using BussinessLogic.DTOs;

namespace BussinessLogic.Services.Interfaces;

public interface IReviewService
{
    Task<ReviewDTO> AddToRecipe(CleanUserDTO userId, Guid recipeId, ReviewAddDTO reviewDto);
    Task<IEnumerable<ReviewDTO>> GetByUser(Guid userId);
    Task<ReviewDTO> UpdateToRecipe(Guid userId, Guid recipeId, ReviewUpdateDTO reviewDto);
    Task DeleteFromRecipe(Guid userId, Guid recipeId);
}
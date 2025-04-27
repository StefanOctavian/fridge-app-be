using BussinessLogic.DTOs;

namespace BussinessLogic.Services.Interfaces;

public interface IReviewService
{
    Task<ReviewDTO> AddToRecipe(UserDTO userId, Guid recipeId, ReviewAddDTO reviewDto);
    Task<IEnumerable<ReviewDTO>> GetByUser(Guid userId);
    Task<ReviewDTO> Update(Guid reviewId, ReviewUpdateDTO reviewDto);
    Task Delete(Guid reviewId);
}
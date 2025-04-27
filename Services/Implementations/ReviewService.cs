using BussinessLogic.Services.Interfaces;
using BussinessLogic.DTOs;
using BussinessLogic.Extensions;

namespace BussinessLogic.Services.Implementations;

public class ReviewService(HttpClient crudClient) : IReviewService
{
    public async Task<IEnumerable<ReviewDTO>> GetByUser(Guid userId)
    {
        return await crudClient.GetAsync($"/Review/User/{userId}")
            .FromJson<IEnumerable<ReviewDTO>>();
    }

    public async Task<ReviewDTO> AddToRecipe(UserDTO user, Guid recipeId, ReviewAddDTO reviewDto)
    {
        return await crudClient.PostAsync($"/Recipe/{recipeId}/Review/User/{user.Id}", reviewDto)
            .FromJson<ReviewDTO>();
    }

    public async Task<ReviewDTO> Update(Guid reviewId, ReviewUpdateDTO reviewDto)
    {
        return await crudClient.PatchAsync($"/Review/{reviewId}", reviewDto)
            .FromJson<ReviewDTO>();
    }

    public async Task Delete(Guid reviewId)
    {
        await crudClient.DeleteAsync($"/Review/{reviewId}").Unpack();
    }
}
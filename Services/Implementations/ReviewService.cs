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

    public async Task<ReviewDTO> AddToRecipe(CleanUserDTO user, Guid recipeId, ReviewAddDTO reviewDto)
    {
        return await crudClient.PostAsync($"/Recipe/{recipeId}/Review/User/{user.Id}", reviewDto)
            .FromJson<ReviewDTO>();
    }

    public async Task<ReviewDTO> UpdateToRecipe(Guid userId, Guid recipeId, ReviewUpdateDTO reviewDto)
    {
        var reviewId = await crudClient.GetAsync($"/Recipe/{recipeId}/Review/User/{userId}")
            .FromJson<ReviewDTO>()
            .ContinueWith(t => t.Result.Id);
        return await crudClient.PatchAsync($"/Review/{reviewId}", reviewDto)
            .FromJson<ReviewDTO>();
    }

    public async Task DeleteFromRecipe(Guid userId, Guid recipeId)
    {
        var reviewId = await crudClient.GetAsync($"/Recipe/{recipeId}/Review/User/{userId}")
            .FromJson<ReviewDTO>()
            .ContinueWith(t => t.Result.Id);
        await crudClient.DeleteAsync($"/Review/{reviewId}").Unpack();
    }
}
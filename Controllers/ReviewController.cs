using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BussinessLogic.Auth;
using BussinessLogic.DTOs;
using BussinessLogic.Services.Interfaces;
using BussinessLogic.Enums;

namespace BussinessLogic.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class ReviewController(
    IUserService userService, 
    IReviewService reviewService
) : AuthorizedController(userService)
{
    [HttpPost("{recipeId:guid}")]
    public async Task<ActionResult<ReviewDTO>> AddToRecipe(
        [FromRoute] Guid recipeId, [FromBody] ReviewAddDTO reviewDto
    )
    {
        var user = await GetCurrentUser();
        var review = await reviewService.AddToRecipe(user, recipeId, reviewDto);
        return Ok(review);
    }

    [HttpPatch("{recipeId:guid}")]
    public async Task<ActionResult<ReviewDTO>> UpdateToRecipe(
        [FromRoute] Guid recipeId, [FromBody] ReviewUpdateDTO reviewDto
    )
    {
        var user = await GetCurrentUser();
        var review = await reviewService.UpdateToRecipe(user.Id, recipeId, reviewDto);
        return Ok(review);
    }

    [HttpDelete("{recipeId:guid}")]
    public async Task<IActionResult> DeleteFromRecipe([FromRoute] Guid recipeId)
    {
        var user = await GetCurrentUser();
        await reviewService.DeleteFromRecipe(user.Id, recipeId);
        return Ok();
    }

    [ActionName("DeleteFromRecipe")]
    [HttpDelete("{recipeId:guid}/ByUser/{userId:guid}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> DeleteFromRecipeByUser(Guid recipeId, Guid userId)
    {
        await reviewService.DeleteFromRecipe(userId, recipeId);
        return Ok();
    }
}


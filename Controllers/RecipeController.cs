using Microsoft.AspNetCore.Mvc;

using BussinessLogic.DTOs;
using BussinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BussinessLogic.Enums;
using BussinessLogic.Auth;

namespace BussinessLogic.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class RecipeController(
    IUserService userService,
    IRecipeService recipeService
) : AuthorizedController(userService)
{
    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult> Add([FromBody] RecipeAddDTO recipe)
    {
        await recipeService.Add(recipe);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDTO>> GetById([FromRoute] Guid id)
    {
        return Ok(await recipeService.GetById(id));
    }

    [HttpGet]
    public async Task<ActionResult<List<RecipePreviewDTO>>> GetAll()
    {
        return Ok(await recipeService.GetAll());
    }

    [HttpGet]
    public async Task<ActionResult<List<RecipePreviewDTO>>> Search(
        [FromQuery] RecipeFiltersDTO filters
    )
    {
        var currentUser = await GetCurrentUser();
        return Ok(await recipeService.Search(currentUser, filters));
    }


    [HttpPatch("{id}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<RecipeDTO>> Update(
        [FromRoute] Guid id, [FromBody] RecipeUpdateDTO recipeUpdates
    )
    {
        return Ok(await recipeService.Update(id, recipeUpdates));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await recipeService.Delete(id);
        return Ok();
    }
}
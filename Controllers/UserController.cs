using BussinessLogic.DTOs;
using BussinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BussinessLogic.Auth;

namespace BussinessLogic.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class UserController(IUserService userService, IReviewService reviewService) : AuthorizedController(userService)
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDTO>> GetById([FromRoute] Guid id)
    {
        return Ok(await userService.GetUser(id));
    }

    [HttpPatch]
    public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] UserUpdateDTO userUpdate)
    {
        var currentUser = await GetCurrentUser();
        return Ok(await userService.UpdateUser(currentUser.Id, userUpdate));
    }

    [HttpGet]
    public async Task<ActionResult<List<FridgeIngredientDTO>>> GetFridge()
    {
        var currentUser = await GetCurrentUser();
        return Ok(await userService.GetFridge(currentUser));
    }

    [HttpGet]
    public async Task<ActionResult<List<ReviewDTO>>> GetReviews()
    {
        var currentUser = await GetCurrentUser();
        return Ok(await reviewService.GetByUser(currentUser.Id));
    }

    [HttpPut]
    public async Task<ActionResult<List<FridgeIngredientDTO>>> PutFridge(
        [FromBody] List<FridgePutIngredientDTO> ingredients
    )
    {
        var currentUser = await GetCurrentUser();
        return Ok(await userService.PutFridge(currentUser, ingredients));
    }

    [HttpPatch]
    public async Task<ActionResult<List<FridgeIngredientDTO>>> UpdateFridge(
        [FromBody] List<FridgeIngredientDeltaDTO> ingredientsDelta
    )
    {
        var currentUser = await GetCurrentUser();
        return Ok(await userService.UpdateFridge(currentUser, ingredientsDelta));
    }
}
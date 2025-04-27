using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BussinessLogic.DTOs;
using BussinessLogic.Services.Interfaces;
using BussinessLogic.Auth;
using BussinessLogic.Entities.Enums;

namespace BussinessLogic.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class IngredientController(IIngredientService ingredientService) : ControllerBase
{

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<IngredientDTO>> GetById([FromRoute] Guid id)
    {
        return Ok(await ingredientService.GetById(id));
    }

    [HttpGet("{name}")]
    [AllowAnonymous]
    public async Task<ActionResult<IngredientDTO>> GetByName([FromRoute] string name)
    {
        return Ok(await ingredientService.GetByName(name));
    }

    [HttpGet]
    public async Task<ActionResult<List<IngredientDTO>>> GetAll()
    {
        return Ok(await ingredientService.GetAll());
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<IngredientDTO>> Add([FromBody] IngredientAddDTO ingredient)
    {
        return Ok(await ingredientService.Add(ingredient));
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<IngredientDTO>> Update([FromRoute] Guid id, [FromBody] IngredientUpdateDTO ingredient)
    {
        return Ok(await ingredientService.Update(id, ingredient));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await ingredientService.Delete(id);
        return Ok();
    }
}
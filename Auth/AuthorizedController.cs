using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

using BussinessLogic.Services.Interfaces;
using BussinessLogic.Errors;
using BussinessLogic.Enums;
using BussinessLogic.DTOs;

namespace BussinessLogic.Auth;

/// <summary>
/// This abstract class is used as a base class for controllers that need to get current information about the user from the database.
/// </summary>
public abstract class AuthorizedController(IUserService userService) : ControllerBase
{
    private UserClaims? _userClaims;
    protected readonly IUserService userService = userService;

    /// <summary>
    /// This method extracts the claims from the JWT into an object.
    /// It also caches the object if used a second time.
    /// </summary>
    protected UserClaims ExtractClaims()
    {
        if (_userClaims != null)
        {
            return _userClaims;
        }

        ClaimsPrincipal claims = User;
        string? claimId    = claims.FindFirstValue(ClaimTypes.NameIdentifier);
        string? claimName  = claims.FindFirstValue(ClaimTypes.Name);
        string? claimEmail = claims.FindFirstValue(ClaimTypes.Email);
        string? claimRole  = claims.FindFirstValue(ClaimTypes.Role);

        if (claimId == null || claimName == null || claimEmail == null || claimRole == null ||
            !Enum.TryParse(claimRole, out UserRole userRole))
        {
            throw new ForbiddenException("The provided authentication token has been tampered");
        }
        Guid userId = Guid.Parse(claimId);
        
        _userClaims = new(userId, claimName, claimEmail, userRole);
        return _userClaims;
    }

    /// <summary>
    /// This method also gets the currently logged user information from the database to provide more information to authorization verifications.
    /// </summary>
    protected Task<CleanUserDTO> GetCurrentUser() => userService.GetUser(ExtractClaims().Id);
}

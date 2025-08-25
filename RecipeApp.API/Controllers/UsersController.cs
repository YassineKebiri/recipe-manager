using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Commands;
using RecipeApp.Application.Common;

namespace RecipeApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _context;

    public UsersController(IMediator mediator, IApplicationDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users
            .Select(u => new
            {
                u.Id,
                u.Username,
                u.Email,
                u.DisplayName,
                u.ProfilePictureUrl,
                u.IsActive,
                u.CreatedAt
            })
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _context.Users
            .Where(u => u.Id == id)
            .Select(u => new
            {
                u.Id,
                u.Username,
                u.Email,
                u.DisplayName,
                u.ProfilePictureUrl,
                u.IsActive,
                u.CreatedAt,
                RecipeCount = u.Recipes.Count
            })
            .FirstOrDefaultAsync();

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUser), new { id = userId }, new { id = userId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
    {
        var command = new UpdateUserCommand
        {
            Id = id,
            DisplayName = request.DisplayName ?? string.Empty,
            ProfilePictureUrl = request.ProfilePictureUrl,
            IsActive = request.IsActive
        };

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id}/recipes")]
    public async Task<IActionResult> GetUserRecipes(Guid id)
    {
        var recipes = await _context.Recipes
            .Where(r => r.AuthorId == id)
            .Select(r => new
            {
                r.Id,
                r.Title,
                r.Description,
                r.PhotoUrl,
                r.IsPublic,
                r.CreatedAt,
                Category = r.Category != null ? r.Category.Name : "Unknown"
            })
            .ToListAsync();

        return Ok(recipes);
    }
}

public class UpdateUserRequest
{
    public string? DisplayName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool IsActive { get; set; } = true;
}
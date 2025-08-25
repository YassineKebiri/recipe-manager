// RecipeApp.API/Controllers/RecipesController.cs - Version corrigée
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Commands;
using RecipeApp.Application.Common;

namespace RecipeApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _context;

    public RecipesController(IMediator mediator, IApplicationDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetRecipes()
    {
        var recipes = await _context.Recipes
            .Where(r => r.IsPublic)
            .Select(r => new
            {
                r.Id,
                r.Title,
                r.Description,
                r.PhotoUrl,
                r.CreatedAt,
                Author = r.Author != null ? r.Author.DisplayName : "Unknown",
                Category = r.Category != null ? r.Category.Name : "Unknown"
            })
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipe(Guid id)
    {
        var recipe = await _context.Recipes
            .Where(r => r.Id == id)
            .Select(r => new
            {
                r.Id,
                r.Title,
                r.Description,
                r.PhotoUrl,
                r.IsPublic,
                r.CreatedAt,
                Author = r.Author != null ? new { r.Author.Id, r.Author.DisplayName, r.Author.ProfilePictureUrl } : null,
                Category = r.Category != null ? new { r.Category.Id, r.Category.Name } : null
            })
            .FirstOrDefaultAsync();

        if (recipe == null)
            return NotFound();

        return Ok(recipe);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeRequest request)
    {
        var command = new CreateRecipeCommand
        {
            Title = request.Title ?? string.Empty,
            Description = request.Description ?? string.Empty,
            PhotoUrl = request.PhotoUrl,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId
        };

        var recipeId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRecipe), new { id = recipeId }, new { id = recipeId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecipe(Guid id, [FromBody] UpdateRecipeRequest request)
    {
        var command = new UpdateRecipeCommand
        {
            Id = id,
            Title = request.Title ?? string.Empty,
            Description = request.Description ?? string.Empty,
            PhotoUrl = request.PhotoUrl,
            CategoryId = request.CategoryId,
            IsPublic = request.IsPublic
        };

        await _mediator.Send(command);
        return NoContent();
    }
}

public class CreateRecipeRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PhotoUrl { get; set; }
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
}

public class UpdateRecipeRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PhotoUrl { get; set; }
    public Guid CategoryId { get; set; }
    public bool IsPublic { get; set; } = true;
}
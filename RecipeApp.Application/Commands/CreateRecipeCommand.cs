using MediatR;

namespace RecipeApp.Application.Commands;

public class CreateRecipeCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
}
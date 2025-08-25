using MediatR;

namespace RecipeApp.Application.Commands
{
    public class UpdateRecipeCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public bool IsPublic { get; set; } = true;
        public Guid? CategoryId { get; set; }
    }
}
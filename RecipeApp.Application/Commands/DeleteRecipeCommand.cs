using MediatR;

namespace RecipeApp.Application.Commands
{
    public class DeleteRecipeCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
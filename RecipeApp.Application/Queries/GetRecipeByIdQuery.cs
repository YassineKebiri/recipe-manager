using MediatR;
using RecipeApp.Application.DTOs;

namespace RecipeApp.Application.Queries
{
    public class GetRecipeByIdQuery : IRequest<RecipeDto?>
    {
        public Guid Id { get; set; }
    }
}
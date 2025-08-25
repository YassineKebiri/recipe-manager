using MediatR;
using RecipeApp.Application.DTOs;

namespace RecipeApp.Application.Queries
{
    public class GetRecipesQuery : IRequest<List<RecipeDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid? CurrentUserId { get; set; } // Pour savoir si l'utilisateur a déjà réagi
    }
}
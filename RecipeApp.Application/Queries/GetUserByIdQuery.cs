using MediatR;
using RecipeApp.Application.DTOs;

namespace RecipeApp.Application.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto?>
    {
        public Guid Id { get; set; }
    }
}
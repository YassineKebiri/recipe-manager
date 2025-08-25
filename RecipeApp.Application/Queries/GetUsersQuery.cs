using MediatR;
using RecipeApp.Application.DTOs;

namespace RecipeApp.Application.Queries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
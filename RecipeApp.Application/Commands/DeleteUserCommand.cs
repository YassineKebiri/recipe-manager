using MediatR;

namespace RecipeApp.Application.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
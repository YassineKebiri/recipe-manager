using MediatR;

namespace RecipeApp.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
    }
}

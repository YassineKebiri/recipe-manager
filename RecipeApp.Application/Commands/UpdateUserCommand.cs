using MediatR;

namespace RecipeApp.Application.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;  // Seuls les champs modifiables
        public string? ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Note: Username et Email ne sont pas modifiables selon votre domain
    }
}
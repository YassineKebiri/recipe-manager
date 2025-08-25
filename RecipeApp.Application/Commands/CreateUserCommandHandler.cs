using MediatR;
using RecipeApp.Application.Common;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(
                request.Username,
                request.Email,
                request.DisplayName
            );

            // Mettre à jour le profil avec la photo si fournie
            if (!string.IsNullOrEmpty(request.ProfilePictureUrl))
            {
                user.UpdateProfile(request.DisplayName, request.ProfilePictureUrl);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}


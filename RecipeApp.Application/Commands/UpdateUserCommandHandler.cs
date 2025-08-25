using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Common;

namespace RecipeApp.Application.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {request.Id} not found");

            // Utiliser la méthode domain pour mettre à jour le profil
            user.UpdateProfile(request.DisplayName, request.ProfilePictureUrl);

            // Gérer le statut actif/inactif
            if (request.IsActive && !user.IsActive)
                user.Activate();
            else if (!request.IsActive && user.IsActive)
                user.Deactivate();

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}


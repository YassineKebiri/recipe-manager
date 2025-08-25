using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Common;

namespace RecipeApp.Application.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {request.Id} not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
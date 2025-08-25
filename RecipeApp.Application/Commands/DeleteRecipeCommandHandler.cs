using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Common;

namespace RecipeApp.Application.Commands
{
    public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteRecipeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (recipe == null)
                throw new KeyNotFoundException($"Recipe with ID {request.Id} not found");

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
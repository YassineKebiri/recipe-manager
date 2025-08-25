using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Common;

namespace RecipeApp.Application.Commands
{
    public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateRecipeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (recipe == null)
                throw new KeyNotFoundException($"Recipe with ID {request.Id} not found");

            // Utiliser la méthode domain pour mettre à jour
            recipe.Update(request.Title, request.Description, request.PhotoUrl, request.CategoryId);

            // Gérer la visibilité
            if (request.IsPublic && !recipe.IsPublic)
                recipe.MakePublic();
            else if (!request.IsPublic && recipe.IsPublic)
                recipe.MakePrivate();

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
using MediatR;
using RecipeApp.Application.Common;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Commands
{
    public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateRecipeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = new Recipe(
                request.Title,
                request.Description,
                request.PhotoUrl,
                request.AuthorId,
                request.CategoryId
            );

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync(cancellationToken);

            return recipe.Id;
        }
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Common;
using RecipeApp.Application.DTOs;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Queries
{
    public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetRecipeByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RecipeDto?> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Category)
                .Include(r => r.Reactions)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (recipe == null)
                return null;

            // Calculer les compteurs de réactions
            var heartCount = recipe.Reactions.Count(r => r.Type == ReactionType.Heart);
            var applauseCount = recipe.Reactions.Count(r => r.Type == ReactionType.Applause);
            var yummyCount = recipe.Reactions.Count(r => r.Type == ReactionType.Yummy);

            return new RecipeDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                PhotoUrl = recipe.PhotoUrl,
                CreatedAt = recipe.CreatedAt,
                Author = new UserDto
                {
                    Id = recipe.Author.Id,
                    Username = recipe.Author.Username,
                    DisplayName = recipe.Author.DisplayName,
                    ProfilePictureUrl = recipe.Author.ProfilePictureUrl,
                    CreatedAt = recipe.Author.CreatedAt
                },
                CategoryName = recipe.Category?.Name,
                HeartCount = heartCount,
                ApplauseCount = applauseCount,
                YummyCount = yummyCount,
                HasUserReacted = false, // TODO: Implémenter avec UserId si nécessaire
                UserReactionType = null // TODO: Implémenter avec UserId si nécessaire
            };
        }
    }
}
using RecipeApp.Domain.Common;

namespace RecipeApp.Domain.Entities
{
    public class Reaction : BaseEntity
    {
        public Guid RecipeId { get; private set; }
        public Guid UserId { get; private set; }
        public ReactionType Type { get; private set; }

        // Navigation properties
        public Recipe Recipe { get; private set; }
        public User User { get; private set; }

        // Private constructor for EF Core
        private Reaction() { }

        public Reaction(Guid recipeId, Guid userId, ReactionType type)
        {
            RecipeId = recipeId;
            UserId = userId;
            Type = type;
        }

        public void ChangeType(ReactionType type)
        {
            Type = type;
        }
    }

    public enum ReactionType
    {
        Heart = 1,
        Applause = 2,
        Yummy = 3
    }
}
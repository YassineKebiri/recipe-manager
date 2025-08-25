using RecipeApp.Domain.Common;
using RecipeApp.Domain.Events;

namespace RecipeApp.Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string PhotoUrl { get; private set; }
        public bool IsPublic { get; private set; }

        // Foreign keys
        public Guid AuthorId { get; private set; }
        public Guid? CategoryId { get; private set; }

        // Navigation properties
        public User Author { get; private set; }
        public Category? Category { get; private set; }
        public ICollection<Reaction> Reactions { get; private set; } = new List<Reaction>();

        // Private constructor for EF Core
        private Recipe() { }

        public Recipe(string title, string description, string photoUrl, Guid authorId, Guid? categoryId = null)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            PhotoUrl = photoUrl ?? throw new ArgumentNullException(nameof(photoUrl));
            AuthorId = authorId;
            CategoryId = categoryId;
            IsPublic = true;

            // Domain Event
            AddDomainEvent(new RecipeCreatedEvent(Id, AuthorId));
        }

        public void Update(string title, string description, string photoUrl, Guid? categoryId = null)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            PhotoUrl = photoUrl ?? throw new ArgumentNullException(nameof(photoUrl));
            CategoryId = categoryId;
        }

        public void MakePrivate() => IsPublic = false;
        public void MakePublic() => IsPublic = true;

        public int GetReactionCount(ReactionType type)
        {
            return Reactions.Count(r => r.Type == type);
        }
    }
}
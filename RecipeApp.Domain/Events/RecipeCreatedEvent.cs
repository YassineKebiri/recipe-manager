namespace RecipeApp.Domain.Events
{
    public class RecipeCreatedEvent : IDomainEvent
    {
        public Guid RecipeId { get; }
        public Guid AuthorId { get; }
        public DateTime OccurredOn { get; }

        public RecipeCreatedEvent(Guid recipeId, Guid authorId)
        {
            RecipeId = recipeId;
            AuthorId = authorId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.DTOs
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public UserDto Author { get; set; } = new();
        public string? CategoryName { get; set; }
        public int HeartCount { get; set; }
        public int ApplauseCount { get; set; }
        public int YummyCount { get; set; }
        public bool HasUserReacted { get; set; }
        public ReactionType? UserReactionType { get; set; }
    }
}
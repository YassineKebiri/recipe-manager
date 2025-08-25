using RecipeApp.Domain.Common;

namespace RecipeApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string DisplayName { get; private set; }
        public string? ProfilePictureUrl { get; private set; }
        public bool IsActive { get; private set; }

        // Navigation properties
        public ICollection<Recipe> Recipes { get; private set; } = new List<Recipe>();
        public ICollection<Reaction> Reactions { get; private set; } = new List<Reaction>();

        // Private constructor for EF Core
        private User() { }

        public User(string username, string email, string displayName)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            IsActive = true;
        }

        public void UpdateProfile(string displayName, string? profilePictureUrl = null)
        {
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            ProfilePictureUrl = profilePictureUrl;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
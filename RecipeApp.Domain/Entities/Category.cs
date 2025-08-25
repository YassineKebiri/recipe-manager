using RecipeApp.Domain.Common;

namespace RecipeApp.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }

        // Navigation properties
        public ICollection<Recipe> Recipes { get; private set; } = new List<Recipe>();

        // Private constructor for EF Core
        private Category() { }

        public Category(string name, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }

        public void Update(string name, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }
    }
}
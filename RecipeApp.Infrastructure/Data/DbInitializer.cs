using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        // Créer la base de données si elle n'existe pas
        await context.Database.EnsureCreatedAsync();

        // Si des catégories existent déjà, ne rien faire
        if (await context.Categories.AnyAsync())
        {
            return;
        }

        // Ajouter des catégories par défaut
        var categories = new[]
        {
            new Category("Entrées", "Plats d'entrée et apéritifs"),
            new Category("Plats principaux", "Plats de résistance"),
            new Category("Desserts", "Desserts et sucreries"),
            new Category("Boissons", "Boissons chaudes et froides"),
            new Category("Salades", "Salades et crudités"),
            new Category("Soupes", "Soupes et potages")
        };

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();
    }
}
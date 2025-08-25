#Recipe Manager - Application Full-Stack

Une application moderne de gestion de recettes développée avec .NET 8 (Clean Architecture) et React TypeScript, démontrant les meilleures pratiques du développement web.


#Aperçu du Projet

Recipe Manager permet aux utilisateurs de créer des comptes et de gérer leurs recettes préférées. L'application offre une interface intuitive pour :

- Gérer les utilisateurs - Créer, modifier, supprimer des profils utilisateurs
- Organiser les recettes - Ajouter des recettes avec ingrédients, instructions, temps de cuisson
- Suivre les statistiques - Dashboard avec métriques en temps réel
- Interface moderne - Design responsive et expérience utilisateur optimale

#Architecture Technique

#Backend (.NET 8 - Clean Architecture)

RecipeApp.Api/              # API Controllers & Configuration
RecipeApp.Application/      # Business Logic & CQRS
Commands/              # Write Operations
Queries/               # Read Operations  
Validators/            # FluentValidation Rules
Events/               # Domain Events
RecipeApp.Domain/          # Entités & Interfaces
RecipeApp.Infrastructure/  # Data Access & External Services


#Frontend (React + TypeScript)

src/
Components/           # Composants React réutilisables
services/            # API Client & HTTP calls
types/              # Interfaces TypeScript
hooks/              # Custom React Hooks
pages/              # Pages principales


#Démarrage Rapide

#Prérequis
- Node.js 18+ 
- .NET 8 SDK
- PostgreSQL 12+
- Git

#Installation

#Cloner le projet
bash
git clone [votre-repo-url]
cd recipe-manager


#Configuration Backend
bash
# Aller dans le dossier API
cd RecipeApp.Api

#Restaurer les packages
dotnet restore

#Configurer la base de données
#Modifier appsettings.json avec votre chaîne de connexion PostgreSQL
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=RecipeAppDb;Username=votre_user;Password=*******"
  }
}

#Appliquer les migrations
dotnet ef database update

#Lancer l'API
dotnet run


L'API sera disponible sur : https://localhost:7001

#Configuration Frontend
bash
# Aller dans le dossier frontend
cd recipe-app-frontend

# Installer les dépendances
npm install

# Configurer l'environnement
echo "REACT_APP_API_URL=https://localhost:7001/api" > .env

# Démarrer l'application
npm start

Le frontend sera disponible sur : http://localhost:3000

#Guide d'Utilisation

#Page d'Accueil
- Dashboard avec statistiques en temps réel
- Aperçu des recettes récentes
- Navigation rapide vers les sections principales

#Gestion des Utilisateurs
1. Cliquez sur "Utilisateurs" dans la navigation
2. Ajouter : Bouton "Nouvel utilisateur"
3. Modifier : Icône crayon sur chaque carte utilisateur
4. Supprimer : Icône corbeille (avec confirmation)

#Gestion des Recettes
1. Cliquez sur "Recettes" dans la navigation
2. Créer : Bouton "Nouvelle recette" (nécessite des utilisateurs)
3. Formulaire complet :
   - Titre et description
   - Temps de cuisson et nombre de portions
   - Niveau de difficulté (Facile/Moyen/Difficile)
   - Liste d'ingrédients
   - Instructions détaillées
   - Attribution à un utilisateur

#Fonctionnalités Techniques

#Backend Achievements
- Clean Architecture - Séparation claire des responsabilités
- CQRS Pattern - Commands pour l'écriture, Queries pour la lecture
- Entity Framework Core - ORM avec migrations automatiques
- FluentValidation - Validation robuste des données
- Domain Events - Architecture événementielle
- Swagger/OpenAPI - Documentation interactive de l'API
- Dependency Injection - Inversion de contrôle native .NET

#Frontend Achievements
- React 18 avec hooks modernes
- TypeScript strict pour la sécurité des types
- Tailwind CSS pour un design moderne et responsive
- Axios pour les appels API avec intercepteurs
- Lucide Icons pour une iconographie cohérente
- State Management local avec useState/useEffect
- Error Handling gracieux avec retry logic

#Structure de Base de Données

#Table Users
sql
- Id (UUID, Primary Key)
- FirstName (String)
- LastName (String)  
- Email (String, Unique)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)
```

#Table Recipes
sql
- Id (UUID, Primary Key)
- Title (String)
- Description (Text)
- Ingredients (Text)
- Instructions (Text)
- CookingTime (Integer, minutes)
- Difficulty (Enum: Easy/Medium/Hard)
- Servings (Integer)
- UserId (UUID, Foreign Key)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)


#API Endpoints

#Users

GET    /api/users           # Liste tous les utilisateurs
GET    /api/users/{id}      # Détails d'un utilisateur
POST   /api/users           # Créer un utilisateur
PUT    /api/users/{id}      # Modifier un utilisateur
DELETE /api/users/{id}      # Supprimer un utilisateur


#Recipes

GET    /api/recipes              # Liste toutes les recettes
GET    /api/recipes/{id}         # Détails d'une recette
GET    /api/recipes/user/{id}    # Recettes d'un utilisateur
POST   /api/recipes              # Créer une recette
PUT    /api/recipes/{id}         # Modifier une recette
DELETE /api/recipes/{id}         # Supprimer une recette


#Dépannage Courant

# L'API ne démarre pas
1. Vérifiez que PostgreSQL est en cours d'exécution
2. Confirmez la chaîne de connexion dans `appsettings.json`
3. Exécutez `dotnet ef database update` pour créer la base

#Le frontend ne se connecte pas à l'API
1. Vérifiez l'URL dans le fichier `.env`
2. Confirmez que l'API fonctionne sur https://localhost:7001
3. Regardez la console du navigateur pour les erreurs CORS

#Problèmes de style Tailwind
1. Vérifiez que Tailwind est correctement installé : `npm list tailwindcss`
2. Confirmez la configuration dans `tailwind.config.js`
3. Redémarrez le serveur de développement

#Améliorations Prévues

### Fonctionnalités
- Authentification utilisateur avec JWT
- Upload d'images pour les recettes
- Système de favoris et notes
- Recherche avancée et filtres
- Partage de recettes entre utilisateurs
- Mode hors ligne avec cache

### Technique
- Tests unitaires complets (Backend + Frontend)
- Tests d'intégration avec TestContainers
- Docker Compose pour déploiement facile
- CI/CD Pipeline avec GitHub Actions
- Monitoring avec Serilog et Application Insights
- Performance optimisations et lazy loading

#Développement

#Commandes Utiles

Backend :
bash
# Créer une migration
dotnet ef migrations add NomMigration

# Mettre à jour la base
dotnet ef database update

# Lancer les tests
dotnet test

# Construire pour production
dotnet build -c Release


Frontend :
bash
# Mode développement
npm start

# Construire pour production
npm run build

# Analyser le bundle
npm run build && npx serve -s build

# Linter TypeScript
npx tsc --noEmit


#Contribution

Ce projet a été développé dans le cadre d'un test technique comme vous avez demandé démontrant :

- Architecture moderne avec Clean Architecture et CQRS
- Bonnes pratiques de développement full-stack
- Technologies récentes (.NET 8, React 18, TypeScript)
- UX/UI soignée avec Tailwind CSS
- Code maintenable et bien documenté



Recipe Manager - Transformez votre passion culinaire en expérience numérique

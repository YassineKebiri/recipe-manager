using FluentValidation;

namespace RecipeApp.Application.Commands
{
    public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
    {
        public CreateRecipeCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Le titre est obligatoire")
                .MaximumLength(100).WithMessage("Le titre ne peut pas dépasser 100 caractères");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La description est obligatoire")
                .MaximumLength(5000).WithMessage("La description ne peut pas dépasser 5000 caractères");

            RuleFor(x => x.PhotoUrl)
                .NotEmpty().WithMessage("La photo est obligatoire")
                .Must(BeAValidUrl).WithMessage("L'URL de la photo n'est pas valide");

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("L'auteur est obligatoire");
        }

        private static bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var result)
                   && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}
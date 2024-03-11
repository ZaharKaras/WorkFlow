using FluentValidation;

namespace RSS.Application.Feeds.Create
{
	public class CreateFeedCommandValidator : AbstractValidator<CreateFeedCommand>
	{
		public CreateFeedCommandValidator()
		{
			RuleFor(c => c.uri)
				.NotEmpty()
				.WithMessage("Uri cannot be null.")

				.Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
				.When(x => !string.IsNullOrEmpty(x.uri))
				.WithMessage("Invalid uri.");
		}
	}
}

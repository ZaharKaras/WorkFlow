using MediatR;
using RSS.Core.Interfaces;

namespace RSS.Application.Feeds.Delete
{
	public class DeleteFeedCommandHandler : IRequestHandler<DeleteFeedCommand>
	{
		private readonly IFeedRepository _feedRepository;

		public DeleteFeedCommandHandler(IFeedRepository feedRepository)
		{
			_feedRepository = feedRepository;
		}

		public async Task Handle(DeleteFeedCommand request, CancellationToken cancellationToken)
		{
			var feed = await _feedRepository.GetByIdAsync(request.id);

			if (feed is null)
			{
				throw new ArgumentException("There is no such id");
			}

			await _feedRepository.DeleteAsync(request.id, cancellationToken);
			await _feedRepository.SaveChangesAsync(cancellationToken);
		}
	}
}

using Board.Core.Exceptions.Cards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.Update
{
	public class UpdateCardHandler : IRequestHandler<UpdateCardCommand>
	{
		private readonly ICardRepository _cardRepository;
		private readonly ICardUserRepository _userRepository;
		private readonly IEmailService _emailService;

		public UpdateCardHandler(
			ICardRepository cardRepository, 
			ICardUserRepository userRepository, 
			IEmailService emailService)
		{
			_cardRepository = cardRepository;
			_userRepository = userRepository;
			_emailService = emailService;
		}

		public async Task Handle(UpdateCardCommand request, CancellationToken cancellationToken)
		{
			var card = await _cardRepository.GetByIdAsync(request.id);

			if (card is null)
			{
				throw new CardNotFoundException(request.id);
			}

			card.SetDescription(request.description);
			card.SetDeadLine(request.deadLine);
			card.ChangeStatus(request.status);
			card.UpdateTitle(request.title);

			_cardRepository.Update(card);

			await _cardRepository.SaveChangesAsync(cancellationToken);
			await NotificateAssignees(request.id);
		}

		private async Task NotificateAssignees(Guid cardId)
		{
			var assignees = await _userRepository.GetByCardIdAsync(cardId);

			foreach(var assignee in assignees)
			{
				_emailService.SendEmail(assignee.ToString(), $"Card: {cardId}", "Card's state was changed");
			}
		}
	}
}

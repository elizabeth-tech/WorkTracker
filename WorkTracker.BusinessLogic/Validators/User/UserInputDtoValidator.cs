using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Contracts.Models.User;
using WorkTracker.DataAccess.Context;

namespace WorkTracker.BusinessLogic.Validators.User
{
    /// <summary>
	/// Валидатор входных данных пользователя
	/// </summary>
    public class UserInputDtoValidator : AbstractValidator<UserInputDto>
    {
        /// <summary>
		/// Контекст БД
		/// </summary>
		private readonly WorkTrackerContext _workTrackerContext;

        /// <summary>
		/// Конструктор
		/// </summary>
        /// <param name="workTrackerContext">Контекст БД</param>
		public UserInputDtoValidator(WorkTrackerContext workTrackerContext)
        {
            _workTrackerContext = workTrackerContext;

            RuleFor(s => s.Name)
                .NotEmpty()
                .WithMessage("Имя обязательно для заполнения");
            RuleFor(s => s.Surname)
               .NotEmpty()
               .WithMessage("Фамилия обязательна для заполнения");
            RuleFor(s => s.Email)
                .NotEmpty()
                .WithMessage("Email обязателен для заполнения")
                .EmailAddress()
                .WithMessage("Введен некорректный email")
                .CustomAsync(BeEmailAddressUniqueAsync);
        }

        /// <summary>
		/// Проверяет, является ли email уникальным
		/// </summary>
		/// <param name="context">Контекст валидатора</param>
		/// <param name="cancellationToken">Токен отмены</param>
		private async Task BeEmailAddressUniqueAsync(
            string email,
            ValidationContext<UserInputDto> context,
            CancellationToken cancellationToken
        )
        {
            var user = await _workTrackerContext.Users.FirstOrDefaultAsync(
                predicate: x => x.Email == email,
                cancellationToken: cancellationToken
            );

            if (user is not null)
            {
                context.AddFailure(
                    propertyName: "email",
                    errorMessage: "Параметр 'email' должен быть уникальным"
                );
            }
        }
    }
}

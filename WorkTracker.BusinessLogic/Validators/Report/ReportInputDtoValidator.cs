using FluentValidation;
using WorkTracker.Contracts.Models.Report;

namespace WorkTracker.BusinessLogic.Validators.Report
{
    /// <summary>
	/// Валидатор входных данных отчетов
	/// </summary>
    public class ReportInputDtoValidator : AbstractValidator<ReportInputDto>
    {
        /// <summary>
		/// Конструктор
		/// </summary>
		public ReportInputDtoValidator()
        {
            RuleFor(s => s.UserId)
                .NotEmpty()
                .WithMessage("ID пользователя, которому принадлежит отчет, обязательно для заполнения");
            RuleFor(s => s.Annotation)
                .NotEmpty()
                .WithMessage("Примечание к отчету обязательно для заполнения");
            RuleFor(s => s.Hours)
                .NotEmpty()
                .WithMessage("Количество отработанных часов обязательно для заполнения");
            RuleFor(s => s.Date)
                .Custom(BeAValidDate);
        }

        /// <summary>
        /// Проверяет корректность даты
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="context">Контекст валидатора</param>
        private static void BeAValidDate(
            DateTime date,
            ValidationContext<ReportInputDto> context
        )
        {
            if (date == DateTime.MinValue)
            {
                context.AddFailure(
                    propertyName: "date",
                    errorMessage: "Параметр 'date' не должен быть пустым"
                );
            }
        }
    }
}

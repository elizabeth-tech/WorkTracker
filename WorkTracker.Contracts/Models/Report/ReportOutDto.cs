namespace WorkTracker.Contracts.Models.Report
{
    public class ReportOutDto
    {
        /// <summary>
        /// ID отчета
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// FK. ID пользователя, которому принадлежит отчет
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Примечание к отчету
        /// </summary>
        public string Annotation { get; set; }

        /// <summary>
        /// Количество отработанных часов
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Дата создания отчета
        /// </summary>
        public DateOnly Date { get; set; }
    }
}

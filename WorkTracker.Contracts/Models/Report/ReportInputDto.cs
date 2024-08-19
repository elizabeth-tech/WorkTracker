namespace WorkTracker.Contracts.Models.Report
{
    /// <summary>
    /// Входная модель данных отчета
    /// </summary>
    public class ReportInputDto
    {
        /// <summary>
        /// ID пользователя, которому принадлежит отчет
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
        public DateTime Date { get; set; }
    }
}

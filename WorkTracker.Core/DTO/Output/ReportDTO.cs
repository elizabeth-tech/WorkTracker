namespace WorkTracker.Core.DTO.Output
{
    public class ReportDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

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
        public DateOnly Date { get; set; }
    }
}

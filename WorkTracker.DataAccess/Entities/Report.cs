namespace WorkTracker.DataAccess.Entities
{
    /// <summary>
    /// Отчет о работе за день
    /// </summary>
    public class Report
    {
        /// <summary>
        /// ID отчета
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// FK. ID пользователя, которому принадлежит отчет
        /// </summary>
        public long UserId { get; set; }

        public User User { get; set; }

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

using System.Reflection;

namespace WorkTracker.BusinessLogic
{
    /// <summary>
    /// Класс для получения объекта Assembly проекта
    /// </summary>
    public static class BusinessLogicAssembly
    {
        /// <summary>
        /// Значение Assembly
        /// </summary>
        public static readonly Assembly Value = typeof(BusinessLogicAssembly).Assembly;
    }
}

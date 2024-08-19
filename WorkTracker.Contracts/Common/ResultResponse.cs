using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json.Serialization;

namespace WorkTracker.Contracts.Common
{
    /// <summary>
    /// Ответ сервиса
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultResponse<T>
    {
        /// <summary>
        /// Результат
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Result { get; set; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Http код
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Ответ с кодом 400
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        /// <param name="errorCode">Код ошибки</param>
        /// <returns></returns>
        public static ResultResponse<T> GetBadRequestResponse(string errorMessage)
        {
            return new ResultResponse<T>()
            {
                ErrorMessage = errorMessage,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        /// <summary>
        /// Неуспешный ответ
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        /// <param name="statusCode">Статус-код ошибки</param>
        /// <returns></returns>
        public static ResultResponse<T> GetFailResponse(string errorMessage, HttpStatusCode statusCode)
        {
            return new ResultResponse<T>()
            {
                Result = default,
                ErrorMessage = errorMessage,
                StatusCode = (int)statusCode
            };
        }

        /// <summary>
        /// Ответ с кодом 500
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        /// <param name="result">Результат</param>
        /// <returns></returns>
        public static ResultResponse<T> GetInternalErrorResponse(string errorMessage, T result = default)
        {
            return new ResultResponse<T>()
            {
                Result = result,
                ErrorMessage = errorMessage,
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        /// <summary>
        /// Ответ с кодом 200 и результатом
        /// </summary>
        /// <param name="result">Результат</param>
        /// <returns></returns>
        public static ResultResponse<T> GetSuccessResponse(T result)
        {
            return new ResultResponse<T>()
            {
                Result = result,
                StatusCode = StatusCodes.Status200OK
            };
        }

        /// <summary>
        /// Кастомный ответ
        /// </summary>
        /// <param name="result">Результат</param>
        /// <param name="statusCode">Http код</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        /// <returns></returns>
        public static ResultResponse<T> GetCustomResponse(T result, int statusCode, string? errorMessage = null)
        {
            return new ResultResponse<T>()
            {
                Result = result,
                ErrorMessage = errorMessage,
                StatusCode = statusCode
            };
        }
    }
}

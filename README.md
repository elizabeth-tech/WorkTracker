# WorkTracker

REST API для учета отработанного времени.

### Стэк
.NET 6, PostgreSQL, Entity Framework, Swagger, Autofac, AutoMapper, FluentValidation, Serilog.

### Описание
REST API с двумя сервисами: пользователи и отчеты. Реализация операций Удаление\Обновление\Создание\Получение. Валидация входных данных с помощью FluentValidation.
Логгирование ошибок, например:  
  
`2024-03-28 12:09:14.619 +07:00 [Error] Class: ReportService; Method: GetReportsOnUserInMonthAsync; Невозможно получить отчеты. Пользователя с ID=90 не существует.; {"userId":90}`

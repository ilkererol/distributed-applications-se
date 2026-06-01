# PetShelterSystem

**Факултетен номер:** 2401321024
**Студент:** Илкер Мехмед

## Описание

PetShelterSystem е уеб приложение за управление на приют за животни. Системата позволява регистрация и управление на животни, осиновители и заявки за осиновяване.

Проектът се състои от две части:
- **PetShelter.Api** — RESTful Web API (back-end) с JWT автентикация
- **PetShelter.Web** — ASP.NET Core Razor Pages приложение (front-end)

## Технологии

- .NET 8
- ASP.NET Core Web API
- ASP.NET Core Razor Pages
- Entity Framework Core
- Microsoft SQL Server
- JWT Authentication
- FluentValidation

## Функционалности

- Регистрация и вход с JWT токен
- Пълни CRUD операции за Animals, Adopters и Applications
- Филтриране по минимум два критерия за всеки модел
- Pagination и сортиране на всички списъци
- Централизирана обработка на грешки според стандарта RFC 7807
- Валидация на всички входни данни
- Асинхронни заявки към базата данни

## Инсталация и стартиране

### Изисквания

- .NET 8 SDK
- SQL Server Express
- Visual Studio или Visual Studio Code

### Стъпки

**1. Клониране на repository-то**

```bash
git clone https://github.com/<username>/course-work.git
cd course-work/implementations/PetShelterSystem
```

**2. Конфигурация на базата данни**

В `PetShelter.Api/appsettings.json` провери connection string-а и го променете за Вашия SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=<твоя-сървър>\\SQLEXPRESS;Database=PetShelterDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**3. Прилагане на миграциите**

```bash
cd PetShelter.Api
dotnet ef database update
```

**4. Стартиране на API-то**

```bash
cd PetShelter.Api
dotnet run --launch-profile https
```

API-то ще бъде достъпно на: `https://localhost:7198`  
Swagger UI: `https://localhost:7198/swagger`

**5. Стартиране на уеб клиента**

В нов терминал:

```bash
cd PetShelter.Web
dotnet run --launch-profile https
```

Приложението ще бъде достъпно на: `https://localhost:7255`

> **Забележка:** API-то трябва да е стартирано преди уеб клиента. Уеб клиентът комуникира с API-то на `https://localhost:7198` (конфигурирано в `PetShelter.Web/appsettings.json` под `ApiSettings:BaseUrl`).
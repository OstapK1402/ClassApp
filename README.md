# ClassApp

## 📖 Опис
**SchoolApp** – це веб-застосунок на ASP.NET MVC для управління навчальним процесом.  
Додаток дозволяє створювати класи, реєструвати учнів та вчителів, призначати домашні завдання, надсилати відповіді, ставити оцінки та формувати розклад.  
Система підтримує кілька ролей користувачів (адміністратор, учитель, учень).

## ⚙️ Технології
- .NET 8 (ASP.NET Core Web App MVC)
- Entity Framework (Code First)
- MS SQL Server
- Identity (авторизація/реєстрація користувачів)
- AutoMapper
- Dependency Injection
- Bootstrap
- Git

## 🚀 Функціонал
- 👤 **Реєстрація та вхід** користувачів  
- 🎓 **Управління класами** (створення/редагування/видалення)  
- 🧑‍🏫 **Реєстрація учнів та вчителів**  
- 📚 **Домашні завдання**: створення, надсилання відповідей  
- 📝 **Оцінювання**: виставлення оцінок за завдання  
- 📅 **Розклад занять** для класів та вчителів  

## 🚀 Як запустити
1. Клонувати репозиторій:
   ```bash
   git clone https://github.com/OstapK1402/ClassApp.git
   cd SchoolApp/Class.App
2. Відкрити файл appsettings.json і змінити ім’я сервера у рядку підключення:
  "ConnectionStrings": {
  "DefaultConnection": "Server=ТВІЙ_СЕРВЕР;Database=SchoolDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
  } Наприклад:
  Server=LAPTOP\SQLEXPRESS;Database=SchoolDB;
3. Запустити застосунок

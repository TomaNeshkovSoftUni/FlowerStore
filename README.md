# Платформа за магазин за цветя

Дипломен проект на **Тома Николаев Нешков**, клас **12В**, ЧПГДН „Софтуни Будител“, гр. София.
Професия: Приложен програмист (481030), специалност: Приложно програмиране (4810301).
Дипломен консултант: **Веселин Бързанов**.

> Статус: **Етап 1 – Концепция и проучване** (проектът е в процес на разработка).

## Кратко описание

Уеб приложение за онлайн магазин за цветя. Клиентите разглеждат каталог с букети, рози, саксийни цветя и подаръчни комплекти, добавят продукти в количка и правят поръчка с избор на адрес и дата на доставка. Служителите управляват продуктите, наличностите и поръчките, а администраторите – и потребителите, ролите и справките за продажбите.

## Предназначение и основна цел

Да замени ръчното приемане на поръчки по телефон и в социалните мрежи с цялостна уеб платформа, която показва актуален каталог, автоматизира поръчките и улеснява работата на магазина.

## Използвани технологии

| Област | Технология |
|---|---|
| Сървърна част | ASP.NET Core MVC (.NET 10) |
| Клиентска част | Razor Views, Bootstrap 5 |
| База данни | MS SQL Server (Entity Framework Core, Code First) |
| Автентикация и роли | ASP.NET Core Identity (роли Клиент, Служител, Администратор)
| Тестване | xUnit, EF Core InMemory |
| CI | GitHub Actions |

## Архитектура

```
src/
  FlowerShop.Data      – entity класове, DbContext, миграции
  FlowerShop.Services  – бизнес логика (услуги) и DTO класове
  FlowerShop.Web       – MVC уеб приложение (контролери, изгледи, area Admin)
tests/
  FlowerShop.Tests     – unit тестове
docs/                  – документация и план
```

## Инструкции за стартиране

Изисквания: [.NET SDK 10](https://dotnet.microsoft.com/download), MS SQL Server (LocalDB, Express или Docker), `dotnet-ef` (`dotnet tool install --global dotnet-ef`).

```bash
git clone <адрес-на-хранилището>
cd FlowerShop

# 1. Настройка на администратор (User Secrets – не се качват в GitHub)
dotnet user-secrets set "Admin:Email" "admin@flowershop.local" --project src/FlowerShop.Web
dotnet user-secrets set "Admin:Password" "<въведете-своя-парола>" --project src/FlowerShop.Web

# 2. Стартиране (миграциите и началните данни се прилагат автоматично при старт)
dotnet run --project src/FlowerShop.Web
```

Приложението се отваря на адреса, изписан в конзолата (напр. `https://localhost:7xxx`).

## Необходими настройки

- Connection string: `src/FlowerShop.Web/appsettings.json` → `ConnectionStrings:DefaultConnection`. По подразбиране е за SQL Server LocalDB. За SQL Server в Docker го заменете през User Secrets:
  ```bash
  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=FlowerShopDb;User Id=sa;Password=<парола>;TrustServerCertificate=True" --project src/FlowerShop.Web
  ```
- Администраторските данни се задават **само** през User Secrets (`Admin:Email`, `Admin:Password`).

## Структура на базата данни

Подробно описание и ER диаграма: [docs/Database.md](docs/Database.md).
Таблици: `Categories`, `Products`, `Orders`, `OrderItems`, `CartItems`, `Reviews` и таблиците на ASP.NET Core Identity.

## Тестови потребители и роли

| Роля | Как се създава |
|---|---|
| `Admin` (Администратор) | автоматично при старт, с данните от User Secrets |
| `Employee` (Служител) | роля, която администраторът ще задава на потребители (Етап 3) |
| `Customer` (Клиент) | чрез регистрация в приложението |

В хранилището няма реални пароли или лични данни.

## Основни функционалности

Реализирани в Етап 1: начална страница, каталог с търсене и филтър по категория, детайли за продукт, регистрация/вход, роли, Admin area (скелет), собствени страници за грешки 404 и 500.

Планирани: количка, поръчки, управление на каталога и поръчките от администратор, отзиви, допълнителни функционалности. Виж [docs/Stage1-Report.md](docs/Stage1-Report.md).

## Тестове

```bash
dotnet test
```

## Автор

**Тома Николаев Нешков**, 12В клас, 2026/2027 учебна година.

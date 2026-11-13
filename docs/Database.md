# База данни – начална версия (Етап 1)

СУБД: MS SQL Server. Подход: Code First с Entity Framework Core. Таблиците на ASP.NET Core Identity (`AspNetUsers`, `AspNetRoles`, `AspNetUserRoles` и др.) се добавят автоматично; `AspNetUsers` е разширена с `FirstName`, `LastName`, `RegisteredOn`.

## ER диаграма

```mermaid
erDiagram
    AspNetUsers ||--o{ Orders : "прави"
    AspNetUsers ||--o{ CartItems : "има в количка"
    AspNetUsers ||--o{ Reviews : "пише"
    Categories  ||--o{ Products : "съдържа"
    Products    ||--o{ CartItems : "в"
    Products    ||--o{ OrderItems : "в"
    Products    ||--o{ Reviews : "получава"
    Orders      ||--|{ OrderItems : "включва"

    AspNetUsers {
        string Id PK
        string Email
        string FirstName
        string LastName
        datetime RegisteredOn
    }
    Categories {
        int Id PK
        string Name UK
        string Description
    }
    Products {
        int Id PK
        string Name
        string Description
        decimal Price
        string ImageUrl
        int StockQuantity
        bool IsAvailable
        datetime CreatedOn
        int CategoryId FK
    }
    Orders {
        int Id PK
        string UserId FK
        datetime CreatedOn
        int Status
        string DeliveryAddress
        string PhoneNumber
        datetime DeliveryDate
        string CardMessage
        decimal TotalPrice
    }
    OrderItems {
        int Id PK
        int OrderId FK
        int ProductId FK
        int Quantity
        decimal UnitPrice
    }
    CartItems {
        int Id PK
        string UserId FK
        int ProductId FK
        int Quantity
    }
    Reviews {
        int Id PK
        int ProductId FK
        string UserId FK
        int Rating
        string Comment
        datetime CreatedOn
    }
```

## Основни правила

- `Categories.Name` е уникално.
- В `CartItems` двойката (`UserId`, `ProductId`) е уникална – един продукт се среща веднъж в количката.
- В `Reviews` двойката (`ProductId`, `UserId`) е уникална – един отзив на потребител за продукт.
- `OrderItems.UnitPrice` пази цената към момента на поръчката.
- Връзките към `Products` от `OrderItems` и към `AspNetUsers` от `Orders` са с `Restrict`, за да не се губи историята на поръчките.
- Паричните стойности са `decimal(18,2)`.

## Създаване и обновяване

```bash
dotnet ef migrations add InitialCreate --project src/FlowerShop.Data --startup-project src/FlowerShop.Web
dotnet ef database update --project src/FlowerShop.Data --startup-project src/FlowerShop.Web
```

Началният каталог (4 категории, 8 продукта), ролите и администраторът се създават от `DbSeeder` при първо стартиране.

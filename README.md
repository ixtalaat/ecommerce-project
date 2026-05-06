# Ecommerce ASP.NET Core MVC Application

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) ![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white) ![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![Stripe](https://img.shields.io/badge/Stripe-008CDD?style=for-the-badge&logo=stripe&logoColor=white) ![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

A comprehensive e-commerce platform built with ASP.NET Core MVC, designed to provide a seamless online shopping experience. This project demonstrates expertise in full-stack web development, including user authentication, product management, payment processing, and administrative controls.

## Overview

This application implements a layered architecture with separation of concerns, utilizing the Repository Pattern and Unit of Work for data access, Entity Framework Core for ORM, and Stripe for secure payment processing. It features role-based access control with distinct areas for customers, administrators, and identity management.

## Features

- **User Authentication & Authorization**: Secure registration, login, and role-based access using ASP.NET Core Identity.
- **Product Management**: Comprehensive catalog with categories, product details, and image uploads.
- **Shopping Cart & Checkout**: Intuitive cart management with integrated Stripe payment gateway.
- **Order Processing**: Complete order lifecycle management from placement to fulfillment.
- **Admin Dashboard**: Administrative interface for managing products, orders, users, and companies.
- **Responsive Design**: Mobile-friendly UI built with Bootstrap and modern web technologies.

## Architecture

The project follows a clean, layered architecture:

- **EcommerceWeb**: Presentation layer with MVC controllers, views, and areas (Admin, Customer, Identity).
- **Ecommerce.Models**: Domain models and view models.
- **Ecommerce.DataAccess**: Data access layer with Entity Framework Core, repositories, and migrations.
- **Ecommerce.Utility**: Shared utilities including email services and configuration settings.

## Technologies Used

- **Backend**: ASP.NET Core MVC, C#
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Payment**: Stripe API
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap
- **Architecture Patterns**: Repository Pattern, Unit of Work
- **Tools**: Visual Studio, Git, NuGet

## Prerequisites

- .NET 8.0 or later
- SQL Server (LocalDB or full instance)
- Stripe account for payment processing
- Visual Studio 2022 or compatible IDE

## Installation & Setup

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/ixtalaat/ecommerce-project.git
   cd ecommerce-project
   ```

2. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

3. **Configure Database**:
   - Update the connection string in `appsettings.json` and `appsettings.Development.json`
   - Apply migrations:
     ```bash
     dotnet ef database update --project Ecommerce.DataAccess
     ```

4. **Configure Stripe**:
   - Add your Stripe publishable and secret keys to `appsettings.json`

5. **Run the Application**:
   ```bash
   dotnet run --project EcommerceWeb
   ```

6. **Access the Application**:
   - Navigate to `https://localhost:5001` (or the configured port)

## Usage

- **Customer Portal**: Browse products, manage cart, and complete purchases
- **Admin Panel**: Access via `/Admin` for product and order management
- **Identity Management**: User registration and profile management

## Project Structure

```
Ecommerce.sln
├── Ecommerce.DataAccess/     # Data layer with EF Core, repositories
├── Ecommerce.Models/         # Domain and view models
├── Ecommerce.Utility/        # Shared utilities and services
├── EcommerceWeb/             # MVC application with areas
│   ├── Areas/
│   │   ├── Admin/           # Administrative interface
│   │   ├── Customer/        # Customer-facing pages
│   │   └── Identity/        # Authentication pages
│   ├── Views/               # Razor views
│   └── wwwroot/             # Static assets
└── README.md
```

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your improvements.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Author

Developed by [Talaat Ramadan] - [LinkedIn Profile](https://linkedin.com/in/yourprofile) | [GitHub](https://github.com/ixtalaat)

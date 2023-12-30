# Ecommerce ASP.NET Core MVC Project

Welcome to our Ecommerce project built on ASP.NET Core MVC! This platform offers a robust solution for buying and selling products, featuring user authentication, product browsing, cart management, checkout processes, order handling, and an admin panel for streamlined management.

## Features

### Key Features

- **User Authentication:** Secure registration and login functionalities.
- **Product Catalog:** Explore a diverse range of products with detailed descriptions.
- **Shopping Cart:** Add items, manage quantities, and seamlessly proceed to checkout.
- **Checkout and Payment:** Effortlessly handle transactions with integrated Stripe payment processing.
- **Order Management:** Track and manage user-placed orders efficiently.
- **Admin Panel:** Empowers admins to efficiently manage products, orders, and user accounts.

## Installation

Get started with our project by following these steps:

1. **Clone the repository:** `git clone https://github.com/ixtalaat/ecommerce-project.git`
2. **Navigate to the project directory:** `cd ecommerce-project`
3. **Install dependencies:** `dotnet restore`
4. **Configure Database Connection:** Update `appsettings.json` with your SQL Server connection string.
5. **Apply migrations:** `dotnet ef database update`
6. **Start the application:** `dotnet run`

## Usage

Here's how to use the application:

- **Access:** Use your web browser and visit `http://localhost:port`.
- **Authentication:** Register for a new account or log in using existing credentials.
- **Shopping:** Browse products, add items to the cart, and proceed to checkout.
- **Admin Panel:** Admin users can access the admin panel at `http://localhost:port/admin` to manage products, orders, and users.

## Technologies Used

The project leverages various technologies, frameworks, and libraries:

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **HTML/CSS/JavaScript**
- **Bootstrap (or other libraries/frameworks)**
- **Stripe (Payment Integration)**
- **SQL Server (Database)**

## Contributing

Interested in contributing? Follow these steps:

- **Fork the repository**
- **Create a new branch**
- **Make changes**
- **Submit a pull request**

## License

This software is licensed under the [MIT License](https://github.com/nhn/tui.editor/blob/master/LICENSE).

# Blazor Stripe Checkout

Blazor Stripe Checkout is a modern web application built using Blazor. It provides a seamless experience for managing products, users, and integrating Stripe for secure payment processing.

## Features

### Product Management
- **Add Products**: Easily add new products using the `AddProducts.razor` page.
- **Update Products**: Modify existing product details via the `UpdateProducts.razor` page.
- **List Products**: View all products in a tabular format using `ListProducts.razor`.
- **Card View**: Display products in a visually appealing card layout with `ListCardProducts.razor`.

### User Management
- **Add Users**: Add new users through the `AddUser.razor` page.
- **Update Users**: Edit user details using the `UpdateUser.razor` page.

### Stripe Integration
- Secure payment processing with Stripe for a smooth checkout experience.

## Prerequisites

- .NET 9 SDK
- Visual Studio 2022 with the following workloads:
  - ASP.NET and web development
- A Stripe account for API keys.
- MudBlazor for UI components.
- Stripe.Net for Stripe integration.
- CsvHelper for CSV file handling.

## Getting Started

### Clone the Repository

````````
git clone https://github.com/donpotts/BlazorProductsStripeCheckout.git
cd BlazorProductsStripeCheckout
````````

### Configuration
1. Add your Stripe API keys in the appropriate configuration file:
   - For web: `appsettings.json`

Example configuration:
```json
{
  "Stripe": {
    "PublishableKey": "your-publishable-key",
    "SecretKey": "your-secret-key"
  }
}

````````

### Build and Run
1. Open the solution in Visual Studio 2022.
2. Restore NuGet packages: __Tools > NuGet Package Manager > Manage NuGet Packages for Solution__.
3. Run the project using __F5__.

## Usage

### Product Management
- Navigate to the **Products** section to add, update, or view products.

### User Management
- Navigate to the **Users** section to manage user information.

### Stripe Checkout
- Select products and proceed to checkout for a secure payment experience.

## Technologies Used

- .NET 9
- Blazor
- Stripe API
- Sqlite

## Acknowledgments

- [Stripe](https://stripe.com) for payment processing.
- MudBlazor for UI components.
- Stripe.Net for Stripe integration.
- CsvHelper for CSV file handling.

## Authentication
This application uses ASP.NET Core Identity for user authentication. To log in, navigate to the login page and enter your credentials.

Administrator

Username: adminUser@example.com

Password: testUser123!

Normal user

Username: normalUser@example.com

Password: testUser123!

📬 Contact
Don Potts - Don.Potts@DonPotts.com


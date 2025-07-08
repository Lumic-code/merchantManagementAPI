# 🚀 Merchant Service API (.NET 8 + Clean Architecture + CQRS + MediatR + Minimal APIs)

This is a sample **Merchant Management API** built with:
- ✅ .NET 8 Web API  
- ✅ Minimal APIs  
- ✅ Clean Architecture  
- ✅ CQRS with MediatR  
- ✅ FluentValidation  
- ✅ In-Memory Database  
- ✅ External API Integration (REST Countries API)  
- ✅ Unit & Integration Tests

---

## 📦 Features
- **Merchant CRUD API** (`Create`, `Read`, `Update`, `Delete`)
- Country validation via external API (`https://restcountries.com`)
- Request validation (email, required fields)
- Modular, maintainable architecture using **CQRS** & **MediatR**
- Easy to swap In-Memory DB with real database (e.g., PostgreSQL, SQL Server)
- Full integration test suite (including validation & error handling)

---

## 🏗️ Tech Stack
- **.NET 8**
- **Minimal APIs**  
- **EF Core In-Memory Database**
- **MediatR** for CQRS  
- **FluentValidation** for input validation
- **xUnit** + **Moq** for testing  

---

## 📂 Project Structure
MerchantManagement.API/ → Minimal API Endpoints
MerchantManagement.App/ → Application Logic (CQRS, Commands, Queries)
MerchantManagement.Infrastructure/ → Data Access (EF Core)
MerchantManagement.Tests/ → Integration & Unit Tests

---

## 🛠️ API Endpoints

| Method | Endpoint             | Description             |
|--------|----------------------|-------------------------|
| POST   | `/api/merchants`     | Create a merchant (with country validation) |
| GET    | `/api/merchants`     | Get all merchants        |
| GET    | `/api/merchants/{id}`| Get merchant by ID       |
| PUT    | `/api/merchants/{id}`| Update merchant          |
| DELETE | `/api/merchants/{id}`| Delete merchant          |

---

## ✅ Validation
- `BusinessName`: Required  
- `Email`: Must be valid email  
- Validates country via **REST Countries API** before creating merchant

---

## 📄 Example Create Request:
```json
{
  "businessName": "My Shop",
  "email": "shop@example.com",
  "phoneNumber": "1234567890",
  "status": "Active",
  "country": "Nigeria"
}
🚀 Running Locally
dotnet build
dotnet run --project MerchantManagement.API

🧪 Running Tests
dotnet test

📝 Notes
Easily extensible for production databases.
Example of external API integration in clean .NET architecture.


📃 License
MIT — free to use and modify.

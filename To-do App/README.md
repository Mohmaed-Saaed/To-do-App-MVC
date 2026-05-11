# To-do App

Simple Razor Pages / MVC To-do application using ASP.NET Core Identity for authentication and role-based authorization.

## Overview
- Users can register, login, and manage their own tasks.
- Admins can manage categories (create, edit, delete).
- Data seeding creates default categories, roles (`Admin`, `User`) and an admin account on startup.

## Authentication & Authorization
- Identity is configured in `Program.cs` and stored via `DAL.DbContexApp.DbContextApp`.
- Registration flow:
  - `AccountController.Register` creates a new `ApplicationUser` (username, email, phone) and persists it with a hashed password.
  - New users are assigned the `User` role.
- Login flow:
  - `AccountController.Login` signs in users with their `UserName` and password (remember-me supported).
  - Note: login expects the stored `UserName` value; the app seeds/updates the admin user to avoid mismatches.

## Seeded data
- Seeded categories: `Personal`, `Work`, `Shopping`, `Others`.
- Seeded roles: `Admin`, `User`.
- Seeded admin user (development only):
  - Email: `admin@gmail.com`
  - UserName: `Admin` (may be updated to match email by seeder)
  - Password: `Admin@123`
- The seeder runs at startup (`To-do App/Data/SeedData.cs`). Change or remove seeding for production.

## Features
- Category management (Admin only)
  - Controller: `CategoryController` protected by `[Authorize(Roles = "Admin")]`.
  - Menu link visibility is role-aware (`_Layout.cshtml`).
- Task management (User role)
  - Controller: `TaskItemController` protected by `[Authorize(Roles = "User")]`.
  - Tasks are scoped per user (uses ClaimTypes.NameIdentifier).
  - Task forms populate category dropdown from seeded categories.

## Running the project
1. Update the database connection string in `appsettings.json` (`DefaultConnection`).
2. Apply migrations (or let seeder call `context.Database.Migrate()` during startup):
   - `dotnet ef database update` (recommended for production control)
3. Run the app from Visual Studio or `dotnet run`.

## Notes & Recommendations
- Change the seeded admin password before using in production.
- Consider setting `UserName = Email` at registration to avoid login mismatches.
- Run seeding only in Development if you don't want automatic changes in production:
  - Wrap the seeder call in `if (app.Environment.IsDevelopment()) { ... }` in `Program.cs`.
- Add logging for failed sign-in attempts and configure lockout/confirmation policies for security.

## Troubleshooting
- "Invalid Login Attempt" even with correct password:
  - Check the `AspNetUsers` table for the stored `UserName` value and use that to log in.
  - Alternatively, change registration/login to use email consistently.
- Roles missing: verify seeder ran and that migrations have been applied.




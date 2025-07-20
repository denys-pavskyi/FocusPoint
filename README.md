# FocusPoint

**FocusPoint** is a desktop application designed to help users manage their work time, tasks, and notes efficiently. Built with WPF and following the MVVM pattern, it integrates a PostgreSQL database to store user data securely.

## Features

- **User Authentication**: Secure login system with hashed password storage.
- **Time Tracking**: Monitor and display the amount of time spent on tasks.
- **Task Management**: Create, view, and manage a list of tasks.
- **Note Taking**: Write and save notes for future reference.
- **Tabbed Interface**: Navigate between Main, Stats, and Settings tabs.
- **Saved Notes**: Access previously saved notes through a dedicated section.

## Technologies Used

- **Frontend**: WPF (Windows Presentation Foundation) with MVVM architecture
- **Backend**: .NET 7+, Entity Framework Core, AutoMapper
- **Database**: PostgreSQL

## Database Schema Overview

- **Users**: Stores user credentials and profile information
- **MainNotes**: Contains user-created notes
- **TaskItems**: Holds task details and statuses
- **WorkSessions**: Logs time tracking sessions
- **WorkStatistics**: Aggregates user work statistics
- **SavedNotes**: Archives of user notes

## Getting Started
# 1. Clone the Repository


git clone https://github.com/denys-pavskyi/FocusPoint.git

# 2. Navigate to the Project Directory

cd focuspoint

# 3. Configure Your Environment
⚠️ This project does not include a shared appsettings.json file.
You need to create your own appsettings.json in the root of the project or in the FocusPoint.PL folder.
It must include a valid connection string to your PostgreSQL database instance.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=FocusPointDb;Username=your_user;Password=your_password"
  }
}
```
# 4. Apply Migrations
Make sure you have the EF Core CLI installed:

```bash
dotnet ef database update
```
# 5. Run the Application

```bash
dotnet run --project FocusPoint.PL
```

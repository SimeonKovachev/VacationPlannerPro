## VacationPlannerPro

VacationPlannerPro is a practical and user-friendly workforce management system designed to help organizations easily handle projects, vacations, teams, and workers. With a focus on simplicity and functionality, the platform streamlines the process of organizing workplace tasks, ensuring secure and efficient management. Built using modern technologies, VacationPlannerPro provides the tools needed to stay organized and improve productivity in any work environment.

---

## Features

*   **Project Management**: Organize and track project progress, deadlines, and team assignments.
*   **Team Management**: Assign leaders, manage worker roles, and monitor team performance.
*   **Vacation Planning**: Approve, monitor, and schedule employee vacations with ease.
*   **Responsive Design**: Fully optimized for desktops, tablets, and mobile devices.
*   **User Management**: Secure authentication and role-based access control (Admin/User).
*   **Error Handling**: Custom pages for 404 and 500 errors for a smooth user experience.
*   **Data Validation**: Both client-side and server-side validation to ensure data integrity.

---

## Technologies Used

### Frameworks and Libraries

*   **Backend**: ASP.NET Core .NET 8.0
*   **Frontend**: Razor View Engine with Bootstrap 5
*   **Database**: Microsoft SQL Server with Entity Framework Core
*   **Authentication**: ASP.NET Identity with roles for User and Administrator

---

## Application Structure

### Models (Entity Framework Core)

*   **Leader**: Manages team leaders with professions and assigned teams.
*   **Team**: Represents groups assigned to projects and leaders.
*   **Project**: Tracks project details, timelines, and associated teams.
*   **Worker**: Stores worker data, professions, and vacation details.
*   **Vacation**: Handles vacation schedules and types, e.g., Paid/Unpaid Leave.
*   **Profession**: Represents worker professions, e.g., Software Engineer, Data Scientist.

### Controllers

1.  **HomeController**: Handles the main pages (Home, Features, Contact, etc.).
2.  **AdminController**: Manages administrative functions within the Admin area.
3.  **ProjectController**: CRUD operations for projects.
4.  **TeamController**: CRUD operations for teams.
5.  **WorkerController**: CRUD operations for workers.
6.  **VacationController**: CRUD operations for vacations.

### Database

*   **Relational Database**: Microsoft SQL Server
*   **Seeding**: Initial data for projects, workers, professions, leaders, and teams is seeded into the database. As well as basic Admin and User Roles

---

## Installation Guide

### Prerequisites

*   **Microsoft SQL Server** installed and running
*   **Visual Studio 2022**
*   **.NET 8.0 SDK**

### Setup Instructions

1.  Clone the repository:
    
    bash
    
    Copy code
    
    `git clone https://github.com/your-repo/VacationPlannerPro.git cd VacationPlannerPro`
    
2.  Add a `secrets.json` file to configure connection strings:
    
    bash
    
    Copy code
    
    `dotnet user-secrets init dotnet user-secrets set "ConnectionStrings:DbConnection" "ExampleConnection;Database=VacationPlannerPro;Trusted_Connection=True;TrustServerCertificate=True"`
    
3.  Apply database migrations:
    
    bash
    
    Copy code
    
    `dotnet ef database update`
    
4.  Run the application:
    
    bash
    
    Copy code
    
    `dotnet run`
    

### User Accounts

*   **Administrator Credentials**:
    *   Email: `admin@example.com`
    *   Password: `Admin123!`
*   **User Credentials**:
    *   Email: `user@example.com`
    *   Password: `User123!`

---

## Application Highlights

### Responsive Design

*   Bootstrap 5 ensures mobile-friendly layouts and excellent UI/UX.

### Pagination and Search

*   Integrated pagination and filtering for large datasets.

### Error Handling

*   **404 Not Found** and **500 Internal Server Error** custom pages for user-friendly feedback.

### Areas

*   **Admin Area**: Separate section for administrative management.

---

## Source Control

*   GitHub Repository: [GitHub Repo Link](https://github.com/your-repo/VacationPlannerPro)
*   **Commit History**: Over 20 commits spread across at least 5 different days.

---

## Additional Features (Bonuses)

*   **AJAX** for dynamic data loading.
*   **Responsive UI** for all device sizes.

---

## Contact Information

For support or contributions, please contact:  
**Email**: `simokovachev04@gmail.com`  
**GitHub**: [GitHub Repo Link](https://github.com/your-repo/VacationPlannerPro)

Thank you for reviewing VacationPlannerPro!

# Restaurante Los Dragones

A full-stack restaurant management system for handling day-to-day operations of a bar and restaurant, including orders, menu, and back-office data management.

> **Note on this repository:** This public repository hosts the **frontend** (HTML / JavaScript / Bootstrap). The system is backed by a separate **.NET 8 / C#** API with a **SQL Server** database, deployed to production on **Somee**. The repository also includes the .NET solution scaffolding (`Bar_Restaurante_Los_Dragones.sln` and the `LosDragones.DAL` data-access layer).
>
> ## Tech Stack
>
> **Frontend**
> ![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=flat&logo=html5&logoColor=white) ![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=flat&logo=javascript&logoColor=black) ![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=flat&logo=bootstrap&logoColor=white)
>
> **Backend**
> ![.NET 8](https://img.shields.io/badge/.NET_8-512BD4?style=flat&logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-512BD4?style=flat&logo=csharp&logoColor=white) ![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat&logo=microsoftsqlserver&logoColor=white) ![Somee](https://img.shields.io/badge/Hosted_on-Somee-2C9CDB?style=flat)
>
> ## Features
>
> - Order management for the bar and restaurant
> - Menu and product administration
> - Persistent storage backed by SQL Server through a dedicated data-access layer
> - Responsive UI built with Bootstrap
> - Production deployment on Somee
>
## Project Structure

```text
Bar_Restaurante_Los_Dragones/        # Frontend (HTML / JS / Bootstrap) + .NET solution
├── Bar_Restaurante_Los_Dragones.sln  # .NET solution file
└── LosDragones.DAL/                  # Data access layer (C# / SQL Server)
```

## Getting Started

### Frontend

Open the HTML pages directly in a browser, or serve them with any static server. Make sure the API base URL in the frontend scripts points to your running backend (local or the Somee deployment).

### Backend (.NET 8 / SQL Server)

```bash
# Restore and build the solution
dotnet restore
dotnet build

# Configure the SQL Server connection string, then run the API
dotnet run
```

## Live Demo

The system is deployed in production on Somee. <!-- TODO: replace with the live URL -->
**Demo:** _add the Somee URL here once confirmed active (e.g. https://losdragones.somee.com)._

## Screenshot

> _Screenshot coming soon. Add an image to the repo and reference it here, e.g.:_ `![Los Dragones screenshot](./screenshot.png)`

## Team

This project was developed as a team effort by:

- Wilson Jesús Gonzaga Cortés
- Carlos Emanuel Jiménez Calvo
- Henry Santiago Ramírez Álvarez
- Karly Narciso Paniagua Madrigal

# Database Sync

[![Build Status](https://dev.azure.com/30CTB/Security-Database-Sync/_apis/build/status/securedevteam.Security-Database-Sync?branchName=master)](https://dev.azure.com/30CTB/Security-Database-Sync/_build/latest?definitionId=3&branchName=master)

The developed software allows synchronization for the N-number of databases with identical tables. The idea of this project was the need for synchronization of MS SQL Server databases, where the server must constantly have up-to-date (updated) data from all other databases. This application has a number of features for synchronization, cleaning and template filling of a specific table in the database. The main application interface is implemented as a Console App.

## Application features
1. Console application to start the selected synchronization;
2. Hard sync;
3. Hard synchronization through Bulk operation;
4. Flexible synchronization;
5. Flexible synchronization through Bulk operation;

## Built With
- [N-Layer architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures);
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/) (Language-Integrated Query) - uniform query syntax in C#;
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - data access technology; 
- [Git](https://git-scm.com/) - version control system;
- [Trello](https://trello.com/en) - a web-based Kanban-style list-making application;
- [Azure Pipelines](https://azure.microsoft.com/en-us/services/devops/) - continuous integration;

## Authors
- [Mikhail M.](https://mikhailmasny.github.io/) - Architect & .NET Developer;
- [Alexandr G.](https://s207883.github.io/) - Full-stack .NET Developer;

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/securedevteam/Security-Database-Sync/blob/master/LICENSE) file for details.

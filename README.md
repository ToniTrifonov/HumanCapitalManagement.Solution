# Human Capital Management

Welcome to the Human Capital Management application!

With this application project managers can create projects, store and manage employee data, and assign employees to those projects.

The site can be launched by setting HumanCapitalManagement.Web project as Startup project.
The Sql Server connection string is configured to work with a local Sql Server. Therefore, if an Sql Server is installed all that needs to be done is writing Update-Database in the Package Manager Console in order for a new database and schema to be created.

There are 2 user roles -> Admin and Project Manager.
On database creation a database seeder creates the main Admin account which can be used for creating other accounts with either Admin or Project Manager role. Each account with Admin role can create new accounts and each account with Project Manager role can create projects and add employees to them.

The following account can be used to login to the site and create other Admins or Project Managers:
Username: admin@admin.com
Password: Test123!

After logging in, an account can be created using the following form:
![image](https://github.com/user-attachments/assets/7bf2c271-28bd-45f5-8c86-fc747cd84023)

In order to start creating projects and adding employees, a new account with the Project Manager role should be created and logged into.
After logging into that account, new projects can be created using the following form:
![image](https://github.com/user-attachments/assets/0c44b509-6eb1-4ff4-abd4-f06ee8dd443f)

New employees can be added to a project after clicking on the name of the project in the projects table:
![image](https://github.com/user-attachments/assets/f87d5d19-f953-48af-b9f3-c799a503e4aa)

Employyes can be edited and can also be deleted.
![image](https://github.com/user-attachments/assets/351fa9bd-3ce8-42c4-b326-582c84c4f670)

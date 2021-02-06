# College-Management-System

Environment & Tools:

1.       .Net Framework 4.8
2.       Visual Studio 2019
3.       Entity Framework 6
4.       MVC 5
5.       Angular JS

Procedure: 
Please download the project and then run database migration please note that you have to select Data\CMS.Repository as a default project. After successful migration data will automatically be inserted into the database and then run project. 

Architecture:

1.	Data -> There are two projects under this folder one is CMS.Data and other one is CMS.Repository. CMS.Data is used for Business Entities and CMS.Repostitry is used for abstracting data access.
2.	Libraries -> There are two projects under this folder one is CMS.Configuration and other one is CMS.Services. CMS.Configuration is used for Messaging purpose and CMS.Services is used for SQL related activities.
3.	Presentation -> This is used for to display View and connect with angular js.  

<p align="center">	
   <a href="https://www.linkedin.com/in/saira-rashid1216/">
      <img alt="Saira Rashid" src="https://img.shields.io/badge/Saira Rashid-0077B5?style=for-the-badge&logo=linkedin&logoColor=white" />
   </a>
   <a href="https://parrotdiseshop.azurewebsites.net">
       <img alt="Parrotdise Shop" src="https://img.shields.io/badge/parrotdiseshop-03C04A?style=for-the-badge&logo=&logoColor=white" />
   </a>
   <a href="mailto:saira.rashid1216@gmail.com">
       <img alt="Saira Rashid Email" src="https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white" />
   </a>
</p>

# ParrotdiseShop - E-commerce Web Application built using ASP.Net Core 6 - MVC and Web API

## Table Of Contents
- [About this project](#about-this-project)
- [Key Features/Concepts Covered/Things Learned](#key-featuresconcepts-coveredthings-learned)
- [Project Details](#project-details)
    - [Customer use-cases](#customer-use-cases)
    - [Employee use-cases](#employee-use-cases)
    - [Admin use-cases](#admin-use-cases)
- [Project Installation Requirements](#project-installation-requirements)
- [Demo Website](#demo-website)

## About this project

This project was developed to document my journey of learning ASP.Net Core and to refresh my skills as a .Net Developer. It is an e-commerce web application that allows customers to make online purchases of parrot toys and treats. It features CRUD functionality to manage product inventory, system users and order processing. 

The application is built using ASP.Net Core 6 MVC and Rest APIs with a MS-SQL database at the back-end. Defining the initial project requirements and setting a scope for the project was the challenging part as well figuring out the integration with Stripe for Payment functionality. Additional challenges were faced when thinking about how to allow guest users to checkout products without having to login. Major portion of the time was spent in database design. 
On the front-end side, currently it uses jquery. Eventually, as I acquire new skills and learn new technologies, I would like to add additional front-end technologies and improve upon it by using one of the popular front-end Javascript frameworks (React, Angular, Vue)

## Key features/Concepts Covered/Things Learned
- N-tier Architecture
- Repository Pattern and Unit Of Work
- TempData in .Net Core 6
- Bootstrap Version 5 for CSS
- Rest APIs
- Razor Pages
- Integration with Sweet Alerts, Toastr Notifications, RichTexEditor using TinyMCE and Datatables.Net
- Figured out How To Scaffold Asp.Net Core Identity (using Razor Pages Framework)
- Successfully completed Integration with Stripe for Payment
- Implemented Third-party authentication using Facebook APIs
- Application deployed to Azure
- Used Jquery Ajax to call APIs
- Used Ef Core Code-First Migrations
- Fluent APIs
- To ensure clear separation of concerns, I employed Dtos and used AutoMapper library

## Project Details
There are three types of users that can access the application:
    1. Admin (highest level of permissions)
    2. Employees (that are not admin)
    3. Customers (can only access application store-front)
      
   ### Customer use-cases
   - Customer can visit the website address of the application and browse for products to buy for pet parrots
   - Customer can select products and their quantities and add them to shopping cart
   - Customer has the choice to login or to checkout as a guest (anonymous) user
   - If customer decides to login, they can do so by registering on our website or by using a third party authentication provider (facebook)
   - Customer pays for their purchases using Stripe
   - A logged in customer has the option to view their order details

  ### Employee use-cases
   - Once logged in, they can manage products, categories of product
   - Employee can view all orders, and update, process, ship or cancel orders if needed
   - Employee can also purchase products from application store-front
 
  ### Admin use-cases
   - Admin can do everything that an employee or customer can do
   - Admin can create/register new employee or admin accounts
   - Admin can also view all user accounts in the system and can lock/unlock them

## Project Installation Requirements
 This application is developed using .NET Core 6. 
 You may need to install [Visual Studio 2022](https://visualstudio.microsoft.com/vs/community/) to be able to run this project code on your local machine. 
 You may also need [SQL Server 2019](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2019) for database.

 ## [Demo Website](https://parrotdiseshop.azurewebsites.net/)
 The application is currently deployed on Azure and is accessible on this url:
 
 [https://parrotdiseshop.azurewebsites.net/](https://parrotdiseshop.azurewebsites.net/)

   ### Sample Admin Credential

  **Username:** admin@parrotdise.com
 
  **Password:** ParrotdiseAdmin123#!

  ### Sample Product Images
   Can be found [here](https://github.com/sairars/ParrotdiseShop/tree/main/Product%20Images)

## Credits
- [.Net Core MVC Course By Bhrugen Patel](https://www.udemy.com/share/101uZQ/)
- [Asp.Net MVC 5 Course By Mosh Hamedani](https://codewithmosh.com/p/asp-net-mvc)
- [Entity Framework Course By Mosh Hamedani](https://codewithmosh.com/p/entity-framework)
- [Introduction to Asp.Net Core - LinkedInLearning](https://www.linkedin.com/learning-login/share?forceAccount=false&redirect=https%3A%2F%2Fwww.linkedin.com%2Flearning%2Fintroducing-asp-dot-net-core%3Ftrk%3Dshare_ent_url%26shareId%3DVfq6nkVKSFeJVGdO1N4aYA%253D%253D)


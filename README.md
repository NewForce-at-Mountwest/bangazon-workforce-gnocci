# Bangazon Workforce Web Application:
## Human Resource Management System (HRMS)

Welcome, new Bangazonians!


### Description

The Bangazon Workforce Management Human Resource Management System (HRMS) allows HR users to View, Create, Edit, and Delete Details / List(s) of: Employees, Departments, Employee-Assigned Computers, and Employee Training Programs.  This system supports HR Departments with hiring Employees, placing Employees (in Departments), allocating equipment (Computers), and providing Employees with professional development opportunities (Training Programs) within Bangazon.


### Software Requirements

- Sql Server Manangment Studio
- Visual Studio Community 2017


### Enitity Relationship Diagram

The [ERD](https://dbdiagram.io/d/5cdbeaf51f6a891a6a654e2) consist of the following tables:
1. Computer
1. ComputerEmployees
1. Customer
1. Department
1. Employee
1. EmployeeTraining
1. Order
1. OrderProduct
1. PaymentType
1. Product
1. ProductType
1. TrainingProgram
![alt text](https://github.com/NewForce-at-Mountwest/bangazon-workforce-gnocci/blob/master/BangazonERD.png?raw=true])


### To Test Locally
If you would like to test locally:

1. git clone this repo
1. cd into bangazon-api-gnocci/BangazonWorkforce
1. start BanazonWorkforce.sln
1. press ctr+f5


#### Computer

Given a user wants to view all Computers

When the user 'clicks' on the "Computer" item in the navigation bar

Then the user should see a list of all Computers

And each item should show a hyperlink (to the right), entitled "Details", that can be 'clicked' to view the details


Given a user is viewing all Computers

When the user clicks the "Create New" link

Then the user should be presented with a form in which the following Computer information can be entered:
- PurchaseDate
- Make
- Manufacturer
The user can then 'click' the "Create" button, to submit the form [data]


Given user is viewing "Details" of a single Computer

Then the user should be presented with a form in which the following information can be entered:
- Id
- PurchaseDate
- Make
- Manufacturer


Given a user is viewing all Computers

When the user 'clicks' on the "Delete" link (to the right)

Then the user should be presented with a screen to verify that it should be deleted

And if the user chooses to 'click' the "Delete" from that screen prompt, the Computer (Id, PurchaseDate, Make, and Manufacturer) should be deleted


### Employee

Given an HR Employee wants to view Employees

When the employee clicks on the "Employees" item in the navigation bar,

Then all current Employees should be listed with the following information:
- Id
- FirstName
- LastName
- Department

Given the user is viewing the list of employees

When the user clicks the "Create New" hyperlink,

Then a form for will be displayed on which the following information can be entered:
- FirstName (text field)
- LastName (text field)
- IsSuperVisor (Checkbox for whether or not the Employee is a Supervisor)
- Department (Select Department from a drop down)
And the user can 'click' the "Create" button to enroll a new Employee


Given a user is viewing the Employee list,

Then the user clicks on the "Details" link, to the left of the Employee being selected,

And the user should be shown a detail view of that employee including:
- Id
- FirstName
- LastName
- IsSuperVisor
- Department


Given user is viewing an Employee

When user clicks on the "Edit" link

Then user should be able to edit the FirstName, LastName, IsSuperVisor, and Departments fields of the Employee, or change the Department 
to which the Employee is currently assigned


#### Department

Given user wants to view all Departments,

When user 'clicks' on the "Department" item in the navigation bar,

All current Departments should be listed

And the following information should be presented to the user:
- [Department] Name
- Size of Department (Total [# of] Employees)
- Dept. Budget
When the user 'clicks' on the "Delete" link (to the right)

Then the user should be presented with a screen to verify that this Department should be deleted

And if the user chooses to 'click' the "Delete" from that screen prompt, the Department (Id, Name, and Budget) should be deleted


Given user is viewing all Departments,

When user 'clicks' on the "Create New Department" link

Then a form should be presented in which the new Department Name can be entered (Name, Budget) with an associated "Create" button


Given user is viewing list of Departments,

When a user 'clicks' on a Department

Then a view should be presented with the Department Name as a header,

And the Dept. (Department) Budget should be listed

And a list of employees ("Team Members) currently assigned to that department should be listed


Given the user wants to see all of the Employees in a department

When the user performs a gesture on the Department Detail affordance

Then the user should see the Department Name

And the user should see the Department Budget

And the user should see the full name of each Employee in that Department


### Training Program

Given a user wants to view all Training Programs

When the user 'clicks' the "Training Programs" link / item in the navigation bar

Then the user will see a list of all Training Programs times


Given the user is viewing all Training Programs

When the user 'clicks' the "Create New" link

Then the user should be presented with a form in which the following information can be entered
- Name
- StartDate
- EndDate
- MaxAttendees


Given user is viewing the list of Training Programs

When the user 'clicks' on the "Details" link (to the right of the page) of a Training Program

Then the user should see all details of that Training Program

And any Employees that are currently scheduled to be Attending the program


Given user is viewing the details of a Training Program

When the user 'clicks' on the "Edit" link (bottom left of page)

Then the user should be presented with a form that allows the user to edit any property of the Training Program, including:
- Name
- StartDate
- EndDate
- MaxAttendees



# ASP.NET MVC Web API Project

## Available Routes for User

| URL                   | Method | Authorize | Description                     |
|-----------------------|--------|-----------|---------------------------------|
| /api/todo             | GET    | User      | Get all user's to-do items      |
| /api/todo/{id}        | GET    | User      | Get a user's to-do item by ID   |
| /api/user             | GET    | User      | Get my user                     |
| /api/login            | POST   | User      | Login                           |
| /api/todo             | POST   | User      | Add a new to-do item to user    |
| /api/todo/{id}        | PUT    | User      | Update user's to-do item        |
| /api/todo/{id}        | DELETE | User      | Delete user's to-do item        |

## Available Routes for Admin

| URL                   | Method | Authorize | Description                      |
|-----------------------|--------|-----------|----------------------------------|
| /api/todo             | GET    | Admin     | Get all user's to-do items        |
| /api/todo/{id}        | GET    | Admin     | Get a user's to-do item by ID     |
| /api/user             | GET    | Admin     | Get all users                     |
| /api/user             | GET    | Admin     | Get my user                       |
| /api/user             | POST   | Admin     | Add a new user                    |
| /api/todo             | POST   | Admin     | Add a new to-do item to user      |
| /api/todo/{id}        | PUT    | Admin     | Update user's to-do item          |
| /api/todo/{id}        | DELETE | Admin     | Delete user's to-do item          |
| /api/user/{id}        | DELETE | Admin     | Delete user and all his to-do's   |

# Server Side Notes

- Only administrators have the permission to add/delete users.
- Users can only manage their own to-do items and are restricted from viewing other users' to-do items.
- Both to-do items and users are stored in a JSON file.
- To-do items and users are accessed through an injected service using an interface for easy transition to a database-based application.
- Implement an extension method of `IServiceCollection` for streamlined registration.
- All requests should be logged, including start date & time, controller & action names, logged-in user (if applicable), and the duration of the operation in milliseconds.

# Client Side Notes

- The default page should display the user's to-do list and allow for adding, updating, and deleting items.
- If there is no logged-in user (no token saved in local storage or expired token), the user should be redirected to a login page instead of the default page.
- If a user has administrative privileges, there should be a navigational link between the to-do list page and the users list page.
- Include a Postman button on the login page.

## Challenges

- Implement the functionality to allow users to update their own details, such as name and password.
- Enable administrators to view and edit their to-do items while behaving as a regular user.
- Implement the ability for users to log in using their Google account.

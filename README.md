<img src="/capstone-project-team-coco/wwwroot/img/wewatchlogo.png" alt="WeWatch logo" style="width: 200px;"/>

##### *A quick app to keep track of the watched shows on any platform and friends you watch them with!* 
##### *Created by Corinna & Osase from **HotCoCoApps** (HotCoCoApps@gmail.com)*
 
****
## What is WeWatch?
We love watching TV and we bet you love watching TV too! **WeWatch** is a fully-responsive, one-step application that helps you keep track of all the shows you are watching and who you're watching with, across ANY platform!

Just log in, add your favourite shows, add *Watchers* (who you're watching with), and connect them! **WeWatch** takes care of the rest! This app is customizable, so you can add, edit, and delete programs or *Watchers* any time!

****
## Features & Functionality (By Page)

**Sign Up & Log In & Log Out**  
Users must first create an account, then log in to begin using **WeWatch**. Emails and passwords are safely hashed for an added layer of security.

**Programs**  
Users can click here to see a list current *Programs*. Click on any program to see all connected Watchers, current season and episode for that show. Users can adjust episodes right from this screen!

***Watchers***  
A list of all the connected *Watchers* you are currently watching with. Click on any watcher to see all connected programs, current seasons and episodes you have with that watcher. 

**Edit Programs**  
Here, a user can add, edit, or delete programs titles, seasons, and episodes.

**Edit *Watchers***  
Update your *Watchers* list easily! Add, edit, or delete who you're watching with.

**Make Connections**  
Once you've added a Program and a Watcher, this is where you connect the two! Just choose a Program title and Watcher, the season and episode you are on and click connect! Now you'll see the connection in the Programs tab!

*****
## About

**[|Keep and eye on our future features on Trello |](https://trello.com/b/IE59VJK5/we-watch)**

Have a suggestion or want to collaborate on future projects contact us at hotcocoapps@gmail.com

****
## Installation  
To run this application, you'll need the following already installed on your computer. 
- Git - **[| Installation Instructions |](https://www.atlassian.com/git/tutorials/install-git)**
- Visual Studio 2019  - **[| Installation Instructions |](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019)**
- XAMMP with Apache and MySQL running - **[| Installation Instructions |](https://www.c-sharpcorner.com/article/how-to-install-and-configure-xampp-in-windows-10/)**


**1. Clone Repository**

1. Launch Visual Studio Code
2. From the file menu select clone repository
3. copy this link to the Repository location 
 https://github.com/TECHCareers-by-Manpower/capstone-project-team-coco.git 
4. In Local path enter a folder on your computer where you'd like the project to live
    (eg. C:\Projects)
You can also click on the ... beside the input field to navigate to the correct folder.
5. Click Clone

**2. Install required packages**

In Visual Studio Code
1. In the Package Manager Console in Visual Studio navigate to your project folder (eg. cd: C:\Projects)
2. Run the following commands:
```
dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package Pomelo.EntityFrameworkCore.MySql

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
**3. Create Database**

In the Package Manager Console run:
```
dotnet ef database update
```

**4. Run our app!**

Click the IIS Express button in the Visual Studio toolbar

A new browser window should appear 

**5. Enjoy!**

****
## Code Citations

This entire project relied on teachings by the TechCareers instructors - Video Recording, GitHub examples and HomeWork Help sessions.
**Thank You** James, Warren, Aaron, Bo, and Tammy.

*Controllers > UserController.cs 

-Citation 1 - line 15
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-3.1
ASP.NET 3.1 set up instructions for creating a session state
Used to pass information of whether a user is logged in or not and their userID.

-Citation 2 - line 190
Https://www.youtube.com/watch?v=gSJFjuWFTdA&list=PLgX_X6wpWU2RZ12GQnTBhVH3DmuTiFwm_&index=1&t=3171s&ab_channel=souravmondal
SHA256 is a hashing algorithm used to secure passwords
The build in Crytography includes this method

*Controllers > ShowController.cs

-Citation 1 - line 24
https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.controller.redirecttoaction?view=aspnet-mvc-5.2
Redirect to Action in different Controllers        

*Models > All*
-Citation 1
https://github.com/TECHCareers-by-Manpower/4.1-MVC/tree/Sep22Practice/MVC_4Point1/Models
Used in class practice coded by James as reference for Model Creation

*Views > ShowCard > ByShows.cshtml & ByWatcher.cshtml
-Citation 1 - line 6
https://stackoverflow.com/questions/16636448/dropdownlist-from-objects-mvc
SelectList overload (data, value, text)

*Views > Shows > ManageShows.cshtml
-Citation 1 - line 4
https://www.sitepoint.com/using-the-html5-constraint-api-for-form-validation/
Restricting title length to a max of 50 characters*


****
## Test Cases & Testing Instructions 
Please refer to detailed Testing Plan located in the Planning folder of this repository.
Test Case 1 Signing Up
- Displays Sign Up Page
- Does not allow submission if any field is left blank
- Error message email and confirmation email do not match (case insensitive) 
- Error message if password and confirmation password do not match (case sensitive)
- Error message if user email is already in the system
- Error message if password does not meet constraints (8 character, one digit, and one uppercase)
- Success if email is not in the system, emails match and passwords match
- Redirects to Log In

Test Case 2 Logging In
- Initial page on launch should be the LogIn page
- Displays Log In Page
- Redirects to Programs screen if user already logged in 
- Error message displayed if incorrect email or password is entered
- Login should redirect to Programs screen upon successfully logging in

Test Case 3 Edit Programs
- Displays a list of all Programs in the Db

Test Case 4 Edit Programs - Add Program
- Expands to add Program form
- Does not allow submission if any field is left blank
- Error message if Title entered already exsist
- Error message if Title is > 50 characters
- Does not allow non numeric in season or episode fields
- Error message if Season or Episode not between 1 and 50
- Adds Program to list of all Programs in Db

Test Case 5 Edit Programs - Edit Program
- Clicking on any Program title expands to edit Program form
- Does not allow submission if field is left blank
- Error message if no changes were detected
- Error message if Title already exists
- Error message if Title is > 50 characters
- Error message if Season already exists with that Program (unless episode only is changed)
- Error message if Season is being changed but has a Connection referencing that Program/Season
- Error message if Season or Episode not between 1 and 50
- Edits the Program information in the Db

Test Case 6 Edit Programs - Add Season
- Does not allow submission if any field is left blank
- Error message if Season already exists with that Program
- Error message if Season or Episode not between 1 and 50
- Clicking + Adds Season information with reference to Program in the Db

Test Case 7 Edit Watcher 
- Displays a list of all Watchers in Db

Test Case 8 Edit Watcher - Add Watcher
- Expands to Add Watcher form
- Does not allow submission if any field is left blank
- Error message if Name already exists in Db
- Error message if Name > 50 characters
- Adds Watcher information to Db

Test case 9 Edit Watcher - Edit Watcher
- Clicking any Watcher name expands to Edit Watcher form
-  Error message if no changes were detected
- Error message if Name already exists in Db
- Error message if Name > 50 characters
- Updates Watcher data in the Db

Test Case 10 Programs
- Displays a list of Programs that user is currently watching with a Watcher
- Clicking on any program title redirects to list of all Connections with that program

Test Case 11 Watchers
- Displays a list of watchers that the user is currently watching a program with
- Clicking on any Watcher name redirects to list of all Connections with that Watcher

Test Case 12 Connection - Create Connection
- Choose a Program and Watcher from populated drop down
    - Does not allow submission if left blank
     - Error message if a Connection exists between Program and Watcher for user in the Db
- Choose a Season from populated drop down
  - Does not allow submission if left blank
- Choose an Episode from the populated drop down
  - Does not allow submission if left blank
- Click Connect
- Error message if any fields left blank
- Adds Connection information to Db
- 
Test Case 13 Connection - Add/Subtract Episodes
- "+" Increases Episode count by one
  - Season rolls to the next higher if max Episode for Season is reached
  - Error message if at max Season and max Episode
  - Updates Season/Episode data in Db
- "-" Decrease Episode count by one
   - Season rolls down to the next lower Season if at Episode 1
   - Error message if at 1 and min Season
   - Updates  Season/Episode data in Db

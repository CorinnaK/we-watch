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

Check out the live preview at https://wewatchtv.herokuapp.com/

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
3. copy this github link to the Repository location 
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

*README.md
-Citation 1
https://dillinger.io/
Online Editor for Markdown

****
## Test Cases & Testing Instructions 
Please refer to detailed Testing Plan located in the Planning folder of this repository.

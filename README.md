<div align = center>
![WeWatch Logo](capstone-project-team-coco/wwwroot/img/wewatchlogo.png)

##### *A quick app to keep track of the watched shows on any platform and friends you watch them with!* 
##### *Created by Corinna & Osase from **HotCoCoApps** (HotCoCoApps@gmail.com)*
 
****
</div>

## What is WeWatch?
We love watching TV and we bet you love watching TV too! **WeWatch** is a fully-responsive, one-step applicatin that helps you keep track of all the shows you are watching and who you're watching with, across ANY platform!

Just log in, add your favourite shows, add *Watchers* (who you're watching with), and connect them! **WeWatch** takes care of the rest! This app is customizable, so you can add, edit, and delete programs or *Watchers* any time!
****
## Installation  
To run this application, you'll need the following already installed on your computer. 
- Git - Installation instructions https://www.atlassian.com/git/tutorials/install-git
- Visual Studio 2019 - Installation instructions https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019
- XAMMP with Apache and MySQL running - Installation instrutions https://www.c-sharpcorner.com/article/how-to-install-and-configure-xampp-in-windows-10/

**[| Installation Instructions|](https://www.atlassian.com/git/tutorials/install-git)**


**1.  Launch Visual Studio 2019**


From the file menu select clone repository
copy this link to the Repository location 
\  https://github.com/TECHCareers-by-Manpower/capstone-project-team-coco.git 
\
In Local path enter a folder on your computer where you'd like the project to live
\eg. C:\Projects
\You can also click on the ... beside the input field to navigate to the correct folder.
\
Click Clone

**2. Install required packages**
**...........**

In the Package Manager Console in Visual Studio

Navigate to your project folder

Run the following commands:

' dotnet add package Microsoft.EntityFrameworkCore.Design  

' dotnet add package Pomelo.EntityFrameworkCore.MySql  

' dotnet add package Microsoft.EntityFrameworkCore.SqlServer  //

**3. Create Database**
/

In the Package Manager Console run:

*  dotnet ef database update  *


**4. Run our app!**
........**

Click the IIS Express button in the Visual Studio toolbar

A new browser window should appear 


**5. In your browser's Developer Tools, change the view to mobile-device of your choice or desktop view & enjoy!**

## Database Construction  
**1. Change server settings**

**2. Add migrations**

*dotnet ef add migrations*

**3. Update database**

*dotnet ef database update*
****
## Features & Functionality (By Page)
**Sign Up & Log In**  
Users must first create an account, then log in to begin using **WeWatch**. Emails and passwords are safely hashed for an added layer of security.

**Programs**  
Users can click here to see a list current shows, connected *Watchers*, current season and episode, and the date it was last updated. Users can adjust episodes right from this screen!

***Watchers***  
A list of all the connected *Watchers* you've added to your profile 

**Edit Programs**  
Here, a user can add, edit, or delete programs titles, seasons, and episodes.

**Edit *Watchers***  
Update your *Watchers* list easily! Add, edit, or delete who you're watching with.

**Make Connections**  
Once you've added a program and a Watcher, this is where you connect the two! Just choose a program (title, season, and episode), choose a Watcher, and click connect! Now you'll see the connection in the Programs tab!

**About**  
Users can click here to read more about the app and to contact us!
****
## Code Citations

Entire Project relied on teachings by the TechCareers instructors - Video Recording, GitHub examples and HomeWork Help sessions
Thank you James, Tammy, Warren, Aaron, and Bo.

*Controllers > UserController.cs*

citation 1 - line #

citation 2 - line #

*Models > Watcher.cs*

citation 1 - line #

## Test Cases & Testing Instructions  
Test Case 1

Test Case 2
\
\
\
**[| Please click here to view our Trello Board |](https://trello.com/b/IE59VJK5/we-watch)**


Trello Link

Keep an eye on our future features at
https://trello.com/b/IE59VJK5/we-watch

Contact

Have a suggestion or want to collabrate on future projects contact us at hotcocoapps@gmail.com


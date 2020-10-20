
[!WeWatch Logo](capstone-project-team-coco/wwwroot/img/wewatchlogo.png)

##### *A quick app to keep track of the watched shows on any platform and friends you watch them with!* 
##### *Created by Corinna & Osase from **HotCoCoApps** (HotCoCoApps@gmail.com)*
 
****
## What is WeWatch?
We love watching TV and we bet you love watching TV too! **WeWatch** is a fully-responsive, one-step applicatin that helps you keep track of all the shows you are watching and who you're watching with, across ANY platform!

Just log in, add your favourite shows, add *Watchers* (who you're watching with), and connect them! **WeWatch** takes care of the rest! This app is customizable, so you can add, edit, and delete programs or *Watchers* any time!
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
    eg. C:\Projects
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
## Features & Functionality (By Page)
#
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


****
## Code Citations

This entire project relied on teachings by the TechCareers instructors - Video Recording, GitHub examples and HomeWork Help sessions.
**Thank You** James, Warren, Aaron, Bo, and Tammy.

*Controllers > UserController.cs*

citation 1 - line #

citation 2 - line #

*Models > Watcher.cs*

citation 1 - line #
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

Test Case 3 Programs
- Displays a list of Programs that user is currently watching with a Watcher
- Clicking on any program title redirects to list of all Connections with that program

Test Case 3 Watchers
- Displays a list of watchers that the user is currently watching a program with
- Clicking on any Watcher name redirects to list of all Connections with that Watcher

Test Case 4 Edit Programs
- Displays a list of all Programs in the Db

Test Case 5 Edit Programs - Add Program
- Expands to add Program form
- Does not allow submission if any field is left blank
- Error message if Title entered already exsist
- Error message if Title is > 50 characters
- Does not alllow non numeric in season or episode fields
- Error message if Season or Episode not between 1 and 50
- Adds Program to list of all Programs in Db

Test Case 6 Edit Programs - Edit Program
- Clicking on any Program title expands to edit Program form
- Does not allow submission if field is left blank
- Error message if no changes were detected
- Error message if Title already exists
- Error message if Title is > 50 characters
- Error message if Season already exists with that Program (unless episode only is changed)
- Error message if Season is being changed but has a Connection referencing that Program/Season
- Error message if Season or Episode not between 1 and 50
- Edits the Program information in the Db

Test Case 7 Edit Programs - Add Season
- Does not allow submission if any field is left blank
- Error message if Season already exists with that Program
- Error message if Season or Episode not between 1 and 50
- Clicking + Adds Season information with reference to Program in the Db

Test Case 8 Edit Watcher 
- Displays a list of all Watchers in Db

Test Case 9 Edit Watcher - Add Watcher
- Expands to Add Watcher form
- Does not allow submission if any field is left blank
- Error message if Name already exists in Db
- Error message if Name > 50 characters
- Adds Watcher information to Db

Test case 10 Edit Watcher - Edit Watcher
- Clicking any Watcher name expands to Edit Watcher form
-  Error message if no changes were detected
- Error message if Name already exists in Db
- Error message if Name > 50 characters
- Updates Watcher data in the Db


## Trello Link

**[|Keep and eye on our futrure features on Trello |](https://trello.com/b/IE59VJK5/we-watch)**

Contact

Have a suggestion or want to collabrate on future projects contact us at hotcocoapps@gmail.com







[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Dillinger is a cloud-enabled, mobile-ready, offline-storage, AngularJS powered HTML5 Markdown editor.

  - Type some Markdown on the left
  - See HTML in the right
  - Magic

# New Features!

  - Import a HTML file and watch it magically convert to Markdown
  - Drag and drop images (requires your Dropbox account be linked)


You can also:
  - Import and save files from GitHub, Dropbox, Google Drive and One Drive
  - Drag and drop markdown and HTML files into Dillinger
  - Export documents as Markdown, HTML and PDF

Markdown is a lightweight markup language based on the formatting conventions that people naturally use in email.  As [John Gruber] writes on the [Markdown site][df1]

> The overriding design goal for Markdown's
> formatting syntax is to make it as readable
> as possible. The idea is that a
> Markdown-formatted document should be
> publishable as-is, as plain text, without
> looking like it's been marked up with tags
> or formatting instructions.

This text you see here is *actually* written in Markdown! To get a feel for Markdown's syntax, type some text into the left window and watch the results in the right.

### Tech

Dillinger uses a number of open source projects to work properly:

* [AngularJS] - HTML enhanced for web apps!
* [Ace Editor] - awesome web-based text editor
* [markdown-it] - Markdown parser done right. Fast and easy to extend.
* [Twitter Bootstrap] - great UI boilerplate for modern web apps
* [node.js] - evented I/O for the backend
* [Express] - fast node.js network app framework [@tjholowaychuk]
* [Gulp] - the streaming build system
* [Breakdance](https://breakdance.github.io/breakdance/) - HTML to Markdown converter
* [jQuery] - duh

And of course Dillinger itself is open source with a [public repository][dill]
 on GitHub.

### Installation

Dillinger requires [Node.js](https://nodejs.org/) v4+ to run.

Install the dependencies and devDependencies and start the server.

```sh
$ cd dillinger
$ npm install -d
$ node app
```

For production environments...

```sh
$ npm install --production
$ NODE_ENV=production node app
```

### Plugins

Dillinger is currently extended with the following plugins. Instructions on how to use them in your own application are linked below.

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |


### Development

Want to contribute? Great!

Dillinger uses Gulp + Webpack for fast developing.
Make a change in your file and instantaneously see your updates!

Open your favorite Terminal and run these commands.

First Tab:
```sh
$ node app
```

Second Tab:
```sh
$ gulp watch
```

(optional) Third:
```sh
$ karma test
```
#### Building for source
For production release:
```sh
$ gulp build --prod
```
Generating pre-built zip archives for distribution:
```sh
$ gulp build dist --prod
```
### Docker
Dillinger is very easy to install and deploy in a Docker container.

By default, the Docker will expose port 8080, so change this within the Dockerfile if necessary. When ready, simply use the Dockerfile to build the image.

```sh
cd dillinger
docker build -t joemccann/dillinger:${package.json.version} .
```
This will create the dillinger image and pull in the necessary dependencies. Be sure to swap out `${package.json.version}` with the actual version of Dillinger.

Once done, run the Docker image and map the port to whatever you wish on your host. In this example, we simply map port 8000 of the host to port 8080 of the Docker (or whatever port was exposed in the Dockerfile):

```sh
docker run -d -p 8000:8080 --restart="always" <youruser>/dillinger:${package.json.version}
```

Verify the deployment by navigating to your server address in your preferred browser.

```sh
127.0.0.1:8000
```

#### Kubernetes + Google Cloud

See [KUBERNETES.md](https://github.com/joemccann/dillinger/blob/master/KUBERNETES.md)


### Todos

 - Write MORE Tests
 - Add Night Mode

License
----

MIT


**Free Software, Hell Yeah!**

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)


   [dill]: <https://github.com/joemccann/dillinger>
   [git-repo-url]: <https://github.com/joemccann/dillinger.git>
   [john gruber]: <http://daringfireball.net>
   [df1]: <http://daringfireball.net/projects/markdown/>
   [markdown-it]: <https://github.com/markdown-it/markdown-it>
   [Ace Editor]: <http://ace.ajax.org>
   [node.js]: <http://nodejs.org>
   [Twitter Bootstrap]: <http://twitter.github.com/bootstrap/>
   [jQuery]: <http://jquery.com>
   [@tjholowaychuk]: <http://twitter.com/tjholowaychuk>
   [express]: <http://expressjs.com>
   [AngularJS]: <http://angularjs.org>
   [Gulp]: <http://gulpjs.com>

   [PlDb]: <https://github.com/joemccann/dillinger/tree/master/plugins/dropbox/README.md>
   [PlGh]: <https://github.com/joemccann/dillinger/tree/master/plugins/github/README.md>
   [PlGd]: <https://github.com/joemccann/dillinger/tree/master/plugins/googledrive/README.md>
   [PlOd]: <https://github.com/joemccann/dillinger/tree/master/plugins/onedrive/README.md>
   [PlMe]: <https://github.com/joemccann/dillinger/tree/master/plugins/medium/README.md>
   [PlGa]: <https://github.com/RahulHP/dillinger/blob/master/plugins/googleanalytics/README.md>

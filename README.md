# ceng-407-408-project-exam-proctor-and-class-assignment
ceng-407-408-project-exam-proctor-and-class-assignment created by GitHub Classroom

# Installation & Compilation Guide

## Prerequisites
•	Visual Studio’s later versions after 2010 should be installed on the computer so that the project can be executed and compiled successfully. Earlier versions of Visual Studio can bring out problems.

•	Operating System should be Windows since the project includes Windows Service.

•	MS-SQL should be installed on the computer so the database (tables, procedures) connection can be provided.

•	Final version of the project is available at link: ........

## Opening the Project in Visual Studio

•	Open Visual Studio.

•	After the Visual Studio is opened, click open project and then choose the ExamProctorandClassAssignment folder.

•	Thereafter the project is opened in Visual Studio, choose Login.aspx as a start page(Figure 1) to start viewing/using from the beginning.
•	After the Login Page is selected as a start page, press the build button which is located at top and shown by a blue circle in Figure 2.
•	Then program is built and run. It can be seen the screen on the chosen browser.


![](https://image.ibb.co/gQdqyy/figure1.jpg)
### Figure 1

![](https://image.ibb.co/kFLQWJ/figure2.jpg)
### Figure 2

## Creating connection MS-SQL and Visual Studio
•	The system is run on web platform. To view data on the screen, database connection should be provided.
•	From Visual Studio, right click on the project -> add new item -> ado.net entity model -> EF designer from database -> choose database name.
Connection is created and web pages can be seen on the browser after building is finished.

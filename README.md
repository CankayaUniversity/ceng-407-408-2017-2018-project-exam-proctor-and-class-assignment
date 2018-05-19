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


![](https://ibb.co/kDk7o8)
### Figure 1

![](https://image.ibb.co/kFLQWJ/figure2.jpg)
### Figure 2

## Creating connection MS-SQL and Visual Studio
•	The system is run on web platform. To view data on the screen, database connection should be provided.

•	From Visual Studio, right click on the project -> Add New Item -> Ado.net Entity Model -> EF Designer from database -> Choose Database Name.

Connection is created and web pages can be seen on the browser after building is finished.


# USER MANUAL

## Website of the Exam Proctor and Class Assignment System

## -LOGIN-

The website has been implemented for the all users. According to user types, the pages that users can view are different. All users have to login to the system with their TC no and password. They be able to reach to their personal page and other pages with clicking the Sign In button (Figure 1). If user clicks on remember me checkbox, browser, TC no textbox will be filled for second or subsequent entries.

![](https://image.ibb.co/d4Ve1T/login.png)
### Figure 1: Login Page

If the user clicks on Sign Up button, he/she should be enter his/her TC no. If the entered TC number is registered into the school’s database, the user will be registered such as Figure 2.

![](https://image.ibb.co/kzvOT8/sign_up.png)
### Figure 2: Sign up Page

If the user clicks on Forgot Password hyperlink, he/she should be enter his/her e-mail address. When the Request button is clicked, a random password will be sent to the email address entered by the user. It is shown in Figure 3.

![](https://image.ibb.co/nAw2MT/forgot_password.png)
### Figure 3: Forgot Password Page

## -HOMEPAGE-

If the user is student, he/she can view created exam dates of taken courses by his/her. If the user is teacher, he/she can view created exam dates of given courses by his/her. (Figure 4 and Figure 5 with the my profile menu)

![](https://image.ibb.co/exthMT/home_page.png)
### Figure 4: Home Page

![](https://image.ibb.co/m94iT8/home_page_profil.png)
### Figure 5: Home Page with my profile menu

## -EXAM REQUEST-

### -Make an Exam Request-

In this page, teachers be able to request for an exam for given course/s by her/his. Firsty, course name should be chosen from the drop down list. After that, exam duration, class type, number of student and assistant should be choose. If teacher does not want assistant for that exam, there is a check box named “I do not want assistant”.(Figure 6, Figure 7, Figure 8, and Figure 9)

![](https://image.ibb.co/n5OMFo/make_an_exam_request_1.png)
### Figure 6: Make Exam Request Page

![](https://image.ibb.co/kSPiT8/make_an_exam_request_2.png)
### Figure 7: Make Exam Request Page - My Courses Step

![](https://image.ibb.co/ms5CMT/make_an_exam_request_3.png)
### Figure 8: Make Exam Request Page - Capacity & Class Step

![](https://image.ibb.co/jpP6gT/make_an_exam_request_4.png)
### Figure 9: Make Exam Request Page - Capacity & Class Step 2


### -My Exam Requests-

### -Other Exam Requests-

## -SETTINGS-

### -Notification Settings-

In this page, user be able to choose approval platform, time for the get information about created exams. User be able to choose default day and time to be get informed. Also user may not want to be get informed about exams. By clicking the checkbox“I want to be informd coming exams”, user the user will not receive information.(Figure 10)

![](https://image.ibb.co/bYWK1T/notification_settings.png)
### Figure 10: Notification Settings Page

### -Password Settings-

In this page, user be able to change his/her password by filling the necessary textboxes like Figure 11.

![](https://image.ibb.co/eH68vo/password_settings.png)
### Figure 11: Password Settings Page

## -ADMIN PANEL-

### -Faculty-
In this page, admin can insert a faculty and list all faculties of the university such as Figure 12.

![](https://image.ibb.co/kfUvao/admin_panel_faculty.png)
### Figure 12: Admin Panel Page - Faculty 

### -Department-

In this page, admin can add a department, and list all departments by choosing the faculty. (Figure 13)

![](https://image.ibb.co/cwyXMT/admin_panel_department.png)
### Figure 13: Admin Panel Page - Department

### -Course-

In this page, admin can add a course, and list all courses by choosing the faculty and department. (Figure 14)

![](https://image.ibb.co/iUiMFo/admin_panel_course.png)
### Figure 14: Admin Panel Page - Course

### -Class-

In this page, admin can add a class, and list all classes of that university. (Figure 15)

![](https://image.ibb.co/c29vao/admin_panel_class.png)
### Figure 15: Admin Panel Page - Class

### -Exam Calendar Request-

In this page, admin can provide the settings about exam dates for each department.(Figure 16)

![](https://image.ibb.co/kzYP1T/2018_05_19.png)
### Figure 16 : Admin Panel Page - Exam Calendar Request 

## -MY PROFILE-

In this page, user can view his/her personal information and change the contact information and photo. (Figure 17, and Figure 18)

![](https://image.ibb.co/c24Ho8/my_profile_1.png)
### Figure 17: My Profile Page

![](https://image.ibb.co/gJFe1T/my_profile_2.png)
### Figure 18: My Profile Page (Continue)

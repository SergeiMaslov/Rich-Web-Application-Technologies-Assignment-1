Rich-Web-Application-Technologies-Assignment-1
==============================================
Features implemented by Sergei Maslov D10121922 - log in/register using JQuery UI and JQuery, chat history back-end
					(including the server side file read/write serialization) 

Features implemented by Kiril Kirilov C09718277 - websockets(SignalR), form submission/getting chat messages with JQuery 
					readme, chat history front-end, gravatar

The project was put to GitHub by Kiril Kirilov, because Sergei Maslov has Perforce, which he is currently using for 
another of his projects.

!Important! the deployed website can be visited on the following url:
http://strange.com.ua/RichWeb/

1.Prerequisites 

Visual studio 2012, .NET framework 4.5 (it usually comes with VS2012)
Alternatively this project can be deployed using IIS. (It is easier with VS2012)

2.Dependencies

This project makes use of SignalR, JQuery, JQuery UI.
All of this are already in the project code, but in case they are missing
they can be downloaded from:

SignalR (using NuGET package manager of VS2012) - Install-Package Microsoft.AspNet.SignalR
JQuery (using NuGET package manager of VS2012) - Install-Package jQuery
     (using website download) - http://jquery.com/download/
JQuery UI - (using NuGET package manager of VS2012) - Install-Package jQuery.UI.Combined
		    (using website download) -  http://jqueryui.com/download/

3.Building

In VS2012 press F6 or from the BUILD menu select BUILD SOLUTION.

4.Deployment

Recommended:
The easiest way to deploy this project is through VS2012. Just press
the button with Green arrow. On its right side is your default website, which
should automatically start and open application. If you get an exception just
in your browser delete everything after the port number.

Not recommended: 
If you want to deploy the project using IIS. Open IIS manager (inetmgr). Create
a new website or reuse an existing one. Point the physical path to the folder containing
the entire project. Verify that the application pool of the website is using .NET framework 4.0.




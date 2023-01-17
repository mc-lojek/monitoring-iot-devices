# Monitoring IoT devices

A simple web application project which was created in a cooperation with [haichangsi](https://github.com/haichangsi) as a final project for one of our subjects. 
The core functionality of the app is to get the measurements from different types of IoT sensors and provide an interface to explore and analyze the data. 

# Stack:

The whole system is divided into a few components, which are:

 - Data generator, which simulates real sensors and generates data for particular number of different type sensors in selected time intervals and sends it through the message broker.
	 - Python3
	 - Pika
 - Queue based message broker, which is the mediator between sensors (producers) and backend application (consumer).
	 - RabbitMQ
 - Backend API - receives measurements from queue, stores it into database and provides a REST API interface to access the data.
	 - ASP.NET Core MVC
	 - Swagger
 - NoSql Database - persists the data given by .net app
	 - MongoDB
 - Frontend app - Reads data through REST API and displays it in user-friendly way in the browser. 
	 - JavaScript
	 - Vue.js

Apart from above there are two docker compositions, which allow to run all of the components at once in an isolated environment. One composition is dedicated to launch on local development machine and the other is dedicated for the university Docker Swarn cluster.

 
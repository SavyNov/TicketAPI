# Ticket.API
This repository features a demo api for a Ticket Station application. Featuring crud operations for the users and the tickets of the database, as well as registering and buying tickets and connceting them to a user The project uses [Postgresql](https://www.postgresql.org/) as a databse. 

# Requirements
For running the api, you will need either a linux or a windows based machine with [Docker](https://www.docker.com/) installed. Having [Postman](https://www.postman.com/) or [Insomnia](https://insomnia.rest/) can also be used to test the ednpoints.

# Setup
For getting the database set up on your machine, running this command at root level is enough:
```
docker compose up
```
Afterwards starting the application through visual studio will connect it to the database and create the tables automatically using the EntityFramework migrations.

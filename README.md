# Webdictaat.Api

[![Build Status](https://travis-ci.org/Webdictaat/Webdictaat.Api.svg?branch=master)](https://travis-ci.org/Webdictaat/Webdictaat.Api)

The Webdictaat API is an interface for managing the contect of a webdictaat. It's an ASP.NET Core MVC web api project, with a generated swagger documentation page. The current production enviroment can be found at http://webdictaat.aii.avans.nl/swagger/ui/index.html.

## Getting started
This project requires ASP.NET core to be installed with a minimum version of 1.0.0-preview2-1-003177.
After cloning this repo, run the 'dotnet restore' command. 

### Development secrets
Instead of storing sensetive information in the web.config, these variables are served as enviromental variables. In a hosting enviroment like IIS, these variables can be stored in configuration settings of you're application. During development, these variables are served via the dotnet secret store. 

https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets

There are 4 secrets that need to be stored in a configuration file: 
* IdentityProviders:Google:ClientId - google OAUTH
* IdentityProviders:Google:ClientSecret - google OAUTH
* ConnectionStrings:DefaultConnection - The connection string to the remote SQL server database
* IdentityProviders:GoogleServiceAccount - google Analytics - the location of the service account configuration file

To recieve the values of these secrets, please contact Stijn Smulders (ssmulder@avans.nl). 

## Databases
The web api is connected to a single database. You can setup your own MS SQL server or use an online database from azure. 

##wwwroot

The root folder contains all the files related to dictaten. 
* Pages
* Images
* Configuration files
* Templates

Be carefull when adding files to this folder to exclude them from any publish activities to the server.

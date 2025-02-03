# RecordShopBackend
## A backend API hosting albums, created using  ASP.NET Core and .NET 8.0 Entity Framework.
### The Record shop contains endpoints to create, update, read, and delete 'album' objects, returned as JSON.

'Learning' French<br/>
In this implementation the database instance is seeded with popular English artists overtly translated into French. For example, 'Lady Gaga' might become 'Madamme Gaga'. In my own usage the database, connected to a front end website functions as a foreign language learning tool, however, there is no backend requirement it be used this way.

Configuring<br/>
The database can be configured to use a (volatile) in-memory database or a persistent database using SQL server. A bool specifying which to use as well as the SQL server connection string should be put in user app settings.

Technology stack<br/>
An MVC architecture is provided through controller, service, and repository classes, with data validation occuring in ther service layer. Data transfer objects are used for posting and putting data and primary keys are not mannually asignable by default. A helath check is provided at '/health'. The endpoints support RESTful API requests over HTTP. Queries can be built for any of the parameters on an album. ORM is used to configure the database and is provided by .NET 8.0 Entity Framework. 

Testing<br/>
A suite of tests is provided for each of the MVC layers using Moq. Where useful, FluentAssertions is used to provide more readable test assertioins. The test suite uses a separate in-memory database which runs independently of the database running the API.


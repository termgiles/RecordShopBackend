# RecordShopBackend
## A backend API hosting albums, created using .NET 8.0 Entity Framework.
### The Record shop contains endpoints to create, update, read, and delete 'album' objects, reuturned as JSON.

'Bad' French<br/>
In this implementation the database instance is seeded with popular English artists 'over'-translated into French. For example, 'Lady Gaga' might become 'Madamme Gaga'. In my own usage the database functions as a foreign language learning tool, however, there is no requirement it be used this way.

Technology stack<br/>
The database can be configured in-memory or using SQL server. A backend MVC architecture is provided through .NET Entity Framework. Endpoint support RESTful API requests over HTTP.

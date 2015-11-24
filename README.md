# Novelist

This is a simplified example of a blog-like application built on top of following libraries and technologies:

- **AngularJS** (core, routing, resources and sanitize)
- **Asp.net MVC** and **Asp.net WebAPI**,
- **LESS** syntax styles
- **NPoco micro ORM** for DAL
- **SQL Database**
- **T4** Visual Studio's automated code generation templating

This application does just basic things, but should do those nicely. Don't expect it to be super robust as it's a proof of concept app.

### Things to note

1. Authentication is very rudimentary and completely stateless from server's perspective. Once the user authenticates (via WebAPI call to get user data based on credentials), user info is preserved on the client and accessible through custom AngularJS `securityService` instance.
2. Client-side input validation is implemented using AngularJS
3. Post editor support extremely rudimentary formatting using regular expressions so it is possible to at least create paragraphs and headings (h2..h6)

    * To **create a heading** start your line with a # (1-6), space and heading text and whole line will become a header with level defined by number of # characters at front although h1 is not allowed in content and will be converted to h2;
    * To **create a paragraph**, you should enter an empty line between individual text blocks (double enter); You can enter several empty lines and they will be regarded as a single empty line, so no empty paragraphs should be created

4. There is no paging in all posts view
5. Errors are usually very unspecified in th UI, but in the most important parts, actual erroneous responses from the server are logged into console window, so they can be investigated further
6. Non-trivial Angular assets (controllers, services) are written as **true Javascript prototypes** which may seem unusual, but they provide several benefits on large apps where controllers can be inherited; Their injections are manually made available to prototype functions, but this could be automated with a more sophisticated code not shown in this project

### Code projects

There are basically **three C# projects** in this solution:

- **Model** which represents defined application model entities and other application-level features
- **Data** which is our DAL that uses T4 to translate database functionality directly to strong types C# calls used by NPoco micro ORM library, and
- **Web** that is the actual web application including RESTful data API<sup>1</sup>

There is alo an **additional *Database* folder** which acts as the fourth project, but is not. It contains all database creation scripts and automated batch files that recreate, restore or upgrade local database or upgrade production database directly from within Visual Studio development environment.

It also helps keeping database scripts versioned while having these things simple and productive.

> <sup>1</sup> API is designed to be completely RESTful but doesn't implement all HTTP verbs apart from those that are used by the web app.

### T4 translation (SPs to C#)

In order to avoid *magic strings/values* when accessing our database via NPoco micro ORM this project utilizes T4 templating engine provided by Visual Studio. This code generator accesses local database stored procedures and translates them to strongly typed C# alternatives, so our backed code only uses C#.

This also helps any inconsistencies that may arise during DB development. If we changed some SP's parameters, their types or removed an SP (or renaming it), we automatically get **compile-time** errors that our DAL calls don't match stored procedures defined on the database.

In order for this to work as expected all stored procedures that we want parsed need to follow this naming convention:
```
Entity_Function
```

Where *Entity* is some application model entity (i.e. *Post*) and *Function* is something that we want to do related to this entity or entity set (i.e. *Create*).
```sql
create procedure Post_Create (
	@Title nvarchar(200),
	@Details nvarchar(max),
	@AuthorId int
)
as
...
```

All these SPs are then translated to *Entity* classes with *Function* methods that use strong type C# parameters semantically matching those of the stored procedure.
```csharp
internal static partial class Post
{
	public static Sql Create(string title, string details, int? authorId)
	{
		// generated implementation
	}
	
	...
}
```
Where `Sql` type is of NPoco library representing a SQL database call.
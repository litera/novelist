# Novelist

This is a simplified example of a blog-like application built on top of following libraries and technologies:

- **AngularJS** (core, routing, resources and sanitize)
- **Asp.net MVC** and **Asp.net WebAPI**,
- **LESS** syntax styles
- **NPoco micro ORM** on
- **SQL Database**
- **T4** Visual Studio's automated code generation templating

It does just basic things, but should do those nicely.

### Projects

There are basically **three C# projects** in this solution:

- **Model** which represents defined application model entities and other application-level features
- **Data** which is our DAL that uses T4 to translate database functionality directly to strong types C# calls used by NPoco micro ORM library, and
- **Web** that is the actual web application

There is alo an **additional *Database* folder** which acts as the fourth project, but is not. It contains all database creation scripts and automated batch files that recreate, restore or upgrade local database or upgrade production database directly from within Visual Studio development environment.

It also helps keeping database scripts versioned while having these things simple and productive.

### T4 translation (SPs to C#)

In order to avoid *magic strings/values* when accessing our database via NPoco micro ORM this project utilizes T4 templating engine provided by Visual Studio. This code generator accesses local database stored procedures and translates them to strongly typed C# alternatives, so our backed code only uses C#.

This also helps any inconsistencies that may arise during DB development. If we changed some SP's parameters, their types or removed an SP (or renaming it), we automatically get **compile-time** errors that our DAL calls don't match stored procedures defined on the database.

In order for this to work as expected all stored procedures that we want parsed need to follow this naming convention:
```
Entity_Function
```

Where *Entity* may some application model entity (i.e. *Post*) and *Function* is something that we want to do related to this entity or entity set (i.e. *Create*).
```
Post_Create
```

All these SPs are then translated to *Entity* classes with *Function* methods (i.e. `Post_Create` will be translated to `Post.Create(strong_type_CLR_parameters_matching_SP_parameters*)`).
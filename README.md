## To Build
    dotnet build

## To Run
    dotnet run --project AlbumServer/AlbumServer.csproj

From here you can use Postman or `curl -k https://localhost:5001/api/albums/2` to interact with the API.

## To Test
    dotnet test test/test.csproj

## DotNet Core WebAPI
This project is built with dotnet Core 2.1.403 (see `dotnet --version` for your local version) and uses the ASP.Net Core "Web API" framework.

NOTE: This was developed on MacOS, but I've confirmed it runs on Windows 10 as well.

Some Web API References:

 - [Create a Web API with ASP.NET Core and Visual Studio Code](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-vsc?view=aspnetcore-2.1)
 - [Build web APIs with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-2.1)
 - [Kestrel web server implementation in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.1)

The initial project structure was created with cmd line `dotnet new webapi -o AlbumServer` and the test project was created with `dotnet new mstest`.

## Resource Identifiers

We are to provide CRUD routes for albums, which implies that they have resource IDs. Though it is tempting to think of Albums as having a composite key based on unique artist name and album names, it doesn't lend itself to idomatic REST resource paths.

I assign numeric album identifiers in the backend and access them via GET `/albums/{id}` (we could also consider using GUID strings as IDs).

Other things I considered but didn't build:

    GET /albums?artist=Queen&album=Jazz

## DESIGN REVIEW

Were this a production application, we'd be using some kind of R/W database provider. For the sake of this exercise, we load albums into an in-memory ConcurrentDictionary, using hard-coded path to a .csv file. ASP.Net Core has deep support for [dependency injection](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1) where services and data providers can be register at server startup injected into the Controllers via parameterized constructors. I dabbled with this, but I'm not very familiar with the myriad options available in ASP.Net Core. Moving this work forward would depend on the kind of database used, so I stopped. 
 
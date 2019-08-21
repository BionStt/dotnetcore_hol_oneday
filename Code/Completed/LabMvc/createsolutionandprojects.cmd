REM .NET Core 2.1 VS 2017
REM dotnet new globaljson --sdk-version 2.1.500
REM .NET Core 2.1 VS 2019 16.0
REM dotnet new globaljson --sdk-version 2.1.600
REM .NET Core 2.1 VS 2019 16.1
dotnet new globaljson --sdk-version 2.1.700

dotnet new sln -n SpyStore.Hol
dotnet new mvc -n SpyStore.Hol.Mvc -au none --no-https  -o .\SpyStore.Hol.Mvc
dotnet sln SpyStore.Hol.sln add SpyStore.Hol.Mvc
dotnet new classlib -n SpyStore.Hol.Dal -o .\SpyStore.Hol.Dal -f netcoreapp2.1
dotnet sln SpyStore.Hol.sln add SpyStore.Hol.Dal
dotnet new classlib -n SpyStore.Hol.Models -o .\SpyStore.Hol.Models -f netcoreapp2.1
dotnet sln SpyStore.Hol.sln add SpyStore.Hol.Models
dotnet new xunit -n SpyStore.Hol.Dal.Tests -o .\SpyStore.Hol.Dal.Tests
dotnet sln SpyStore.Hol.sln add SpyStore.Hol.Dal.Tests

dotnet add SpyStore.Hol.Mvc reference SpyStore.Hol.Models
dotnet add SpyStore.Hol.Mvc reference SpyStore.Hol.Dal

dotnet add SpyStore.Hol.Dal reference SpyStore.Hol.Models

dotnet add SpyStore.Hol.Dal.tests reference SpyStore.Hol.Models
dotnet add SpyStore.Hol.Dal.tests reference SpyStore.Hol.Dal

dotnet add SpyStore.Hol.Mvc package AutoMapper
dotnet add SpyStore.Hol.Mvc package Newtonsoft.Json
dotnet add SpyStore.Hol.Mvc package LigerShark.WebOptimizer.Core
dotnet add SpyStore.Hol.Mvc package LigerShark.WebOptimizer.sass -v 1.0.34-beta

dotnet add SpyStore.Hol.Dal package Microsoft.EntityFrameworkCore.SqlServer -v 2.1.1
dotnet add SpyStore.Hol.Dal package Microsoft.EntityFrameworkCore.Design -v 2.1.1
dotnet add SpyStore.Hol.Dal package Newtonsoft.Json
dotnet add SpyStore.Hol.Dal package Microsoft.EntityFrameworkCore.Tools -v 2.1.1

dotnet add SpyStore.Hol.Models package Microsoft.EntityFrameworkCore.Abstractions -v 2.1.1
dotnet add SpyStore.Hol.Models package AutoMapper
dotnet add SpyStore.Hol.Models package Newtonsoft.Json

dotnet add SpyStore.Hol.Dal.Tests package Microsoft.EntityFrameworkCore.SqlServer -v 2.1.1




FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
LABEL maintainer="vina"
WORKDIR /src

COPY Directory.Packages.props Kr.Carevo.UMR.sln ./

COPY Kr.Carevo.UMR.Domain/*.csproj ./Kr.Carevo.UMR.Domain/
COPY Kr.Carevo.UMR.Infrastructure/*.csproj ./Kr.Carevo.UMR.Infrastructure/
COPY Kr.Carevo.UMR.Persistence/*.csproj ./Kr.Carevo.UMR.Persistence/
COPY Kr.Carevo.UMR.Application/*.csproj ./Kr.Carevo.UMR.Application/
COPY Kr.Carevo.UMR.Api/*.csproj ./Kr.Carevo.UMR.Api/
COPY Tests/Kr.Carevo.UMR.UnitTests/*.csproj ./Tests/Kr.Carevo.UMR.UnitTests/

RUN dotnet restore Kr.Carevo.UMR.sln

COPY . .

WORKDIR /src/Kr.Carevo.UMR.Api
RUN dotnet publish -c Release --no-restore -o /core

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /core
EXPOSE 8080

COPY --from=build /core .
ENTRYPOINT ["dotnet", "Kr.Carevo.UMR.Api.dll"]
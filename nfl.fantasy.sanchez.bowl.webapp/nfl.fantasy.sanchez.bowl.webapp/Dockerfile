FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY nfl.fantasy.sanchez.bowl.webapp/nfl.fantasy.sanchez.bowl.webapp.csproj nfl.fantasy.sanchez.bowl.webapp/
COPY nfl.fantasy.sanchez.bowl.domain/nfl.fantasy.sanchez.bowl.domain.csproj nfl.fantasy.sanchez.bowl.domain/
RUN dotnet restore
COPY . .
WORKDIR /src/nfl.fantasy.sanchez.bowl.webapp
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "nfl.fantasy.sanchez.bowl.webapp.dll"]

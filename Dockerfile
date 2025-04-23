FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY Todo.Web/Todo.Web.csproj Todo.Web/
RUN dotnet restore Todo.Web/Todo.Web.csproj

COPY . .
WORKDIR /src/Todo.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Todo.Web.dll"]
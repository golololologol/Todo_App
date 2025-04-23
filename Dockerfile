FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Todo.Web.Api/Todo.Web.Api.csproj", "Todo.Web.Api/"]
COPY ["Todo.Application/Todo.Application.csproj", "Todo.Application/"]
COPY ["Todo.Domain/Todo.Domain.csproj", "Todo.Domain/"]
COPY ["Todo.Infrastructure/Todo.Infrastructure.csproj", "Todo.Infrastructure/"]
COPY ["Todo.Web/Todo.Web.csproj", "Todo.Web/"]
RUN dotnet restore "Todo.Web.Api/Todo.Web.Api.csproj"

COPY . .
RUN dotnet publish "Todo.Web.Api/Todo.Web.Api.csproj" \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Todo.Web.Api.dll"]
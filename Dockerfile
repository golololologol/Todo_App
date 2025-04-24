# --- Multi-stage Dockerfile to build API and MVC Web in one container ---
    FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
    WORKDIR /src
    
    # Copy project files for dependency restore
    COPY ["Todo.Application/Todo.Application.csproj", "Todo.Application/"]
    COPY ["Todo.Domain/Todo.Domain.csproj", "Todo.Domain/"]
    COPY ["Todo.Infrastructure/Todo.Infrastructure.csproj", "Todo.Infrastructure/"]
    COPY ["Todo.Web.Api/Todo.Web.Api.csproj", "Todo.Web.Api/"]
    COPY ["Todo.Web/Todo.Web.csproj", "Todo.Web/"]
    
    RUN dotnet restore "Todo.Web.Api/Todo.Web.Api.csproj"
    
    # Copy entire source
    COPY . .
    
    # Publish API
    RUN dotnet publish "Todo.Web.Api/Todo.Web.Api.csproj" -c Release -o /app/api
    # Publish MVC Web
    RUN dotnet publish "Todo.Web/Todo.Web.csproj" -c Release -o /app/web
    
    # --- Final stage ---
    FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
    WORKDIR /app
    RUN apt-get update && DEBIAN_FRONTEND=noninteractive apt-get install -y curl && rm -rf /var/lib/apt/lists/*
    
    # Copy published outputs
    COPY --from=build /app/api ./api
    COPY --from=build /app/web ./web
    
    # Create startup script to run both services
    RUN echo '#!/bin/sh' > run.sh && \
        echo 'export ASPNETCORE_ENVIRONMENT=Development' >> run.sh && \
        echo 'export ASPNETCORE_URLS=http://+:5000' >> run.sh && \
        echo 'dotnet ./api/Todo.Web.Api.dll &' >> run.sh && \
        echo 'echo "Waiting for API to become responsive..."' >> run.sh && \
        echo 'until curl -sSf http://127.0.0.1:5000/swagger/index.html > /dev/null; do echo "."; sleep 1; done' >> run.sh && \
        echo 'echo "API is up, starting MVC UI"' >> run.sh && \
        echo 'cd web' >> run.sh && \
        echo 'export ASPNETCORE_ENVIRONMENT=Development' >> run.sh && \
        echo 'export ASPNETCORE_URLS=http://+:$PORT' >> run.sh && \
        echo 'export WEB_API_URL=http://127.0.0.1:5000/' >> run.sh && \
        echo 'dotnet Todo.Web.dll' >> run.sh && \
        chmod +x run.sh
    
    # Expose ports for API (5000) and allow UI to bind via $PORT
    EXPOSE 5000
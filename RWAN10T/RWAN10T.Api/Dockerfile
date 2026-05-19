# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

# -----------------------------
# BUILD
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia csproj e restaura dependências
COPY ["RWAN10T.Api/RWAN10T.Api.csproj", "RWAN10T.Api/"]
RUN dotnet restore "./RWAN10T.Api/RWAN10T.Api.csproj"

# Copia TODO o projeto (incluindo db/migrations)
COPY . .

WORKDIR "/src/RWAN10T.Api"
RUN dotnet build "./RWAN10T.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# -----------------------------
# PUBLISH
# -----------------------------
FROM build AS publish

ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RWAN10T.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# -----------------------------
# FINAL (RUNTIME)
# -----------------------------
FROM base AS final

WORKDIR /app

# App publicado
COPY --from=publish /app/publish .

# 🔥 GARANTE QUE MIGRATIONS VÃO PRO CONTAINER FINAL
COPY RWAN10T.Api/db ./db

ENTRYPOINT ["dotnet", "RWAN10T.Api.dll"]
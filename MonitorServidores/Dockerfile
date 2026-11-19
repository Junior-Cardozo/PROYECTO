# ETAPA 1: Compilar la API con .NET 10

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copiar el archivo del proyecto
COPY MonitorServidores.csproj ./

# Restaurar dependencias
RUN dotnet restore

# Copiar el codigo fuente
COPY . .

# Compilar en modo Release
RUN dotnet build -c Release -o /app/build

# Publicar la aplicacion
RUN dotnet publish -c Release -o /app/publish --no-restore


# ETAPA 2: Ejecutar la API

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

# Copiar los archivos publicados
COPY --from=build /app/publish .

# Configurar puerto HTTP
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

# Ejecutar con usuario no administrador
USER $APP_UID

# Iniciar nuestra API
ENTRYPOINT ["dotnet", "MonitorServidores.dll"]
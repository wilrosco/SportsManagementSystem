# Etapa 1: Compilación de la aplicación
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app

# Copiar archivos de solución y restaurar dependencias
COPY *.sln .
COPY GestionDeportiva/*.csproj ./GestionDeportiva/
COPY GestionDeportiva/packages.config ./GestionDeportiva/
RUN nuget restore

# Copiar el resto del código y compilar
COPY . .
RUN msbuild GestionDeportiva.sln /p:Configuration=Release /p:OutputPath=/app/output

# Etapa 2: Configuración del servidor en ejecución (Runtime)
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019
WORKDIR /inetpub/wwwroot

# Copiar los archivos compilados desde la etapa anterior
COPY --from=build /app/output/_PublishedWebsites/GestionDeportiva .

# Exponer el puerto estándar para que Railway lo detecte
EXPOSE 80
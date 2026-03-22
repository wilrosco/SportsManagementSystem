# Compilación
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app


COPY *.sln .
COPY *.csproj .
COPY packages.config .
RUN nuget restore

# Copiar el resto del código y compilar
COPY . .
RUN msbuild GestionDeportiva.sln /p:Configuration=Release /p:OutputPath=/app/output

# Etapa 2: Configuración del servidor (Runtime)
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019
WORKDIR /inetpub/wwwroot

# Copiar los archivos compilados
COPY --from=build /app/output/_PublishedWebsites/GestionDeportiva .

# Exponer el puerto estándar
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /api

# Copier le fichier sln et les fichiers csproj d'abord (cache Docker)
COPY *.slnx ./
COPY IncidentAPI/*.csproj IncidentAPI/
COPY AppTests/*.csproj AppTests/

# Récupérer les dépendances
RUN dotnet restore

# Copier tout le reste
COPY . .

# Publier uniquement l'API (important)
RUN dotnet publish IncidentAPI/IncidentAPI.csproj -c Release -o /app/publish

# Préparer l'env. d'exécution (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Forcer l'API dans le conteneur d'être accessible depuis la machine hôte
# et d'écouter sur le port 80
ENV ASPNETCORE_URLS=http://0.0.0.0:80
EXPOSE 80

# Copier les fichiers publiés de l'application depuis l'étape de build (/app/publish)
# vers le dossier courant du conteneur afin de les exécuter
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "IncidentAPI.dll"]

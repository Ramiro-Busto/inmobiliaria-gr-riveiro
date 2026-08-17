# Build: compila la API con el SDK completo.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Inmobiliaria.Api.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app --no-restore

# Runtime: imagen final, mucho más liviana, solo con lo necesario para ejecutar.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_ENVIRONMENT=Production

# Render inyecta la variable PORT y espera que la app escuche justo ahí.
CMD dotnet Inmobiliaria.Api.dll --urls http://0.0.0.0:$PORT

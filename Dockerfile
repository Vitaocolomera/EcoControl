FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/EcoControl.API/EcoControl.API.csproj", "src/EcoControl.API/"]
RUN dotnet restore "src/EcoControl.API/EcoControl.API.csproj"

COPY . .
WORKDIR /src/src/EcoControl.API
RUN dotnet publish "EcoControl.API.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EcoControl.API.dll"]

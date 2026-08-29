# --- PASO 1: Build the application ---

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


# --- PASO 2: Copy the project file and restore dependencies ---
COPY CotizacionService.csproj ./ 
COPY NuGet.Config ./
RUN dotnet restore "CotizacionService.csproj"


# --- PASO 3: Copy the rest of the application and build it ---
COPY . .
RUN dotnet publish "CotizacionService.csproj" -c Release -o /app/publish --no-restore


# --- PASO 4: Create the runtime image ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .


## --- PASO 5: Set environment variables and expose the port ---
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "CotizacionService.dll"]



FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Products_Microsservice.Api/Products_Microsservice.Api.csproj","Products_Microsservice/"]
RUN dotnet restore "./Products_Microsservice.Api/ ./Products_Microsservice.Api.csproj"
COPY . .
WORKDIR "/src/Products_Microsservice.Api"
RUN dotnet build "./Products_Microsservice.Api.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Products_Microsservice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Products_Microsservice.Api.dll" ]
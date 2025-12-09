# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Tüm dosyaları kopyala (UDEApp.sln ana dizinde!)
COPY . .

# Solution ana dizinde → restore burada yap!
RUN dotnet restore "UDEApp.sln"

# Publish et (proje Server klasöründe)
RUN dotnet publish "UDEApp.Server/UDEApp.Server.csproj" -c Release -o /app/publish --no-restore

# 2. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "UDEApp.Server.dll"]
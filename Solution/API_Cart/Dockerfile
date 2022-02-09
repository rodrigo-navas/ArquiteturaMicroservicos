#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["API_Cart/API_Cart.csproj", "API_Cart/"]
RUN dotnet restore "API_Cart/API_Cart.csproj"
COPY . .
WORKDIR "/src/API_Cart"
RUN dotnet build "API_Cart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_Cart.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Cart.dll"]
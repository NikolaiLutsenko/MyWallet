#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MyWallet/MyWallet.csproj", "MyWallet/"]
COPY ["MyWallet.Data/MyWallet.Data.csproj", "MyWallet.Data/"]
COPY ["MyWallet.Services/MyWallet.Services.csproj", "MyWallet.Services/"]
RUN dotnet restore "MyWallet/MyWallet.csproj"
COPY . .
WORKDIR "/src/MyWallet"
RUN dotnet build "MyWallet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyWallet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyWallet.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/AplicationDDD.WebApi/AplicationDDD.WebApi.csproj", "Services/AplicationDDD.WebApi/"]
COPY ["Data/AplicationDDD.DAL/AplicationDDD.DAL.csproj", "Data/AplicationDDD.DAL/"]
COPY ["Services/AplicationDDD.Interfaces/AplicationDDD.Interfaces.csproj", "Services/AplicationDDD.Interfaces/"]
COPY ["Services/AplicationDDD.Interfaces.Base/AplicationDDD.Interfaces.Base.csproj", "Services/AplicationDDD.Interfaces.Base/"]
COPY ["Common/AplicatioDDD.Domain/AplicatioDDD.Domain.csproj", "Common/AplicatioDDD.Domain/"]
COPY ["Common/AplicationDDD.Domain.Base/AplicationDDD.Domain.Base.csproj", "Common/AplicationDDD.Domain.Base/"]
COPY ["Data/AplicationDDD.DAL.MsQLServer/AplicationDDD.DAL.MsQLServer.csproj", "Data/AplicationDDD.DAL.MsQLServer/"]
COPY ["Services/AplicationDDD.Services/AplicationDDD.Services.csproj", "Services/AplicationDDD.Services/"]
COPY ["ApplicationDDD.DAL.SqlLite/ApplicationDDD.DAL.SqlLite.csproj", "ApplicationDDD.DAL.SqlLite/"]
RUN dotnet restore "Services/AplicationDDD.WebApi/AplicationDDD.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/AplicationDDD.WebApi"
RUN dotnet build "AplicationDDD.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AplicationDDD.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AplicationDDD.WebApi.dll"]
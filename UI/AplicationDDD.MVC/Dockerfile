#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UI/AplicationDDD.MVC/AplicationDDD.MVC.csproj", "UI/AplicationDDD.MVC/"]
COPY ["Services/AplicationDDD.WebApe.Client/AplicationDDD.WebApe.Client.csproj", "Services/AplicationDDD.WebApe.Client/"]
COPY ["Services/AplicationDDD.Interfaces/AplicationDDD.Interfaces.csproj", "Services/AplicationDDD.Interfaces/"]
COPY ["Services/AplicationDDD.Interfaces.Base/AplicationDDD.Interfaces.Base.csproj", "Services/AplicationDDD.Interfaces.Base/"]
COPY ["Common/AplicatioDDD.Domain/AplicatioDDD.Domain.csproj", "Common/AplicatioDDD.Domain/"]
COPY ["Common/AplicationDDD.Domain.Base/AplicationDDD.Domain.Base.csproj", "Common/AplicationDDD.Domain.Base/"]
RUN dotnet restore "UI/AplicationDDD.MVC/AplicationDDD.MVC.csproj"
COPY . .
WORKDIR "/src/UI/AplicationDDD.MVC"
RUN dotnet build "AplicationDDD.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AplicationDDD.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AplicationDDD.MVC.dll"]
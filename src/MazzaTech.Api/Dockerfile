#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MazzaTech.Api/MazzaTech.Api.csproj", "src/MazzaTech.Api/"]
RUN dotnet restore "src/MazzaTech.Api/MazzaTech.Api.csproj"
COPY . .
WORKDIR "/src/src/MazzaTech.Api"
RUN dotnet build "MazzaTech.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MazzaTech.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MazzaTech.Api.dll"]
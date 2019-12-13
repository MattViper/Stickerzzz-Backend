FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY Stickerzzz.Core.csproj src/Stickerzzz.Core/
COPY Stickerzzz.Infrastructure.csproj src/Stickerzzz.Infrastructure/
COPY Stickerzzz.Web.csproj src/Stickerzzz.Web/

RUN dotnet restore "src/Stickerzzz.Web/Stickerzzz.Web.csproj"
COPY . Stickerzzz.Web
WORKDIR "src/Stickerzzz.Web"
RUN dotnet build "Stickerzzz.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Stickerzzz.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stickerzzz.Web.dll"]

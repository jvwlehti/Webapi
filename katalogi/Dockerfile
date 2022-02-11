FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["katalogi.csproj", "./"]
RUN dotnet restore "katalogi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "katalogi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "katalogi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "katalogi.dll"]

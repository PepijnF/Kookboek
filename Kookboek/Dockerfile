FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Kookboek/Kookboek.csproj", "Kookboek/"]
COPY ["AbstractionLayer/AbstractionLayer.csproj", "AbstractionLayer/"]
COPY ["LogicLayer/LogicLayer.csproj", "LogicLayer/"]
COPY ["DataLayer/DataLayer.csproj", "DataLayer/"]
RUN dotnet restore "Kookboek/Kookboek.csproj"
COPY . .
WORKDIR "/src/Kookboek"
RUN dotnet build "Kookboek.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kookboek.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kookboek.dll"]

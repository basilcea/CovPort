FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM node:14-alpine as build-node
WORKDIR /Client
COPY src/Client/package.json .
COPY src/Client/package-lock.json .
RUN npm install
COPY src/Client/ . 
RUN npm run build  

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENV BuildingDocker true
WORKDIR /src
COPY . .
RUN dotnet restore "src/Server/Api/Api.csproj"
RUN dotnet build "src/Server/Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Server/Api/Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "Api.dll"]

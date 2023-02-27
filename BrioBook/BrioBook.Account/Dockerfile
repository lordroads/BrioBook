#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BrioBook.Client/BrioBook.Client.csproj", "BrioBook.Client/"]
COPY ["BrioBook.Users.DAL/Brio.Database.DAL.csproj", "BrioBook.Users.DAL/"]
RUN dotnet restore "BrioBook.Client/BrioBook.Client.csproj"
COPY . .
WORKDIR "/src/BrioBook.Client"
RUN dotnet build "BrioBook.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrioBook.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrioBook.Client.dll"]
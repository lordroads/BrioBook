#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Brio.Confirm/Brio.Confirmation.csproj", "Brio.Confirm/"]
COPY ["BrioBook.Users.DAL/Brio.Database.DAL.csproj", "BrioBook.Users.DAL/"]
RUN dotnet restore "Brio.Confirm/Brio.Confirmation.csproj"
COPY . .
WORKDIR "/src/Brio.Confirm"
RUN dotnet build "Brio.Confirmation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Brio.Confirmation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Brio.Confirmation.dll"]
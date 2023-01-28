#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Presentation/Arcadian.WebAPI/Arcadian.WebAPI.csproj", "Src/Presentation/Arcadian.WebAPI/"]
COPY ["Src/Core/Arcadian.Application/Arcadian.Application.csproj", "Src/Core/Arcadian.Application/"]
COPY ["Src/Core/Arcadian.Domain/Arcadian.Domain.csproj", "Src/Core/Arcadian.Domain/"]
COPY ["Src/Infrastructure/Arcadian.Data/Arcadian.Data.csproj", "Src/Infrastructure/Arcadian.Data/"]
RUN dotnet restore "Src/Presentation/Arcadian.WebAPI/Arcadian.WebAPI.csproj"
COPY . .
WORKDIR "/src/Src/Presentation/Arcadian.WebAPI"
RUN dotnet build "Arcadian.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Arcadian.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arcadian.WebAPI.dll"]
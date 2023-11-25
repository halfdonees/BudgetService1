FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ConsoleApp8/ConsoleApp8.csproj", "ConsoleApp8/"]
RUN dotnet restore "ConsoleApp8/ConsoleApp8.csproj"
COPY . .
WORKDIR "/src/ConsoleApp8"
RUN dotnet build "ConsoleApp8.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleApp8.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp8.dll"]

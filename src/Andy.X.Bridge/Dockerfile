# Buildersoft Andy X Bridge Docker File

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Andy.X.Bridge/Andy.X.Bridge.csproj", "src/Andy.X.Bridge/"]
COPY ["src/Andy.X.Bridge.IO/Andy.X.Bridge.IO.csproj", "src/Andy.X.Bridge.IO/"]
COPY ["src/Andy.X.Bridge.Core/Andy.X.Bridge.Core.csproj", "src/Andy.X.Bridge.Core/"]
RUN dotnet restore "src/Andy.X.Bridge/Andy.X.Bridge.csproj"
COPY . .
WORKDIR "/src/src/Andy.X.Bridge"
RUN dotnet build "Andy.X.Bridge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Andy.X.Bridge.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Andy.X.Bridge.dll"]
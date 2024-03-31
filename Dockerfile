#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["wcc.core.api/wcc.core.api.csproj", "wcc.core.api/"]
COPY ["wcc.core.kernel/wcc.core.kernel.csproj", "wcc.core.kernel/"]
COPY ["wcc.core.data/wcc.core.data.csproj", "wcc.core.data/"]
# COPY ["wcc.core.integrations/wcc.core.integrations.csproj", "wcc.core.integrations/"]
COPY ["wcc.core/wcc.core.csproj", "wcc.core/"]
RUN dotnet restore "wcc.core.api/wcc.core.api.csproj"
COPY . .
WORKDIR "/src/wcc.core.api"
RUN dotnet build "wcc.core.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wcc.core.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
# COPY cert /usr/local/cert
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wcc.core.api.dll"]
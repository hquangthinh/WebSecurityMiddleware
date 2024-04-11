FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebSecurityMiddleware.SampleWebApi/WebSecurityMiddleware.SampleWebApi.csproj", "WebSecurityMiddleware.SampleWebApi/"]
COPY ["WebSecurityMiddleware/WebSecurityMiddleware.csproj", "WebSecurityMiddleware/"]
RUN dotnet restore "WebSecurityMiddleware.SampleWebApi/WebSecurityMiddleware.SampleWebApi.csproj"
COPY . .
WORKDIR "/src/WebSecurityMiddleware.SampleWebApi"
RUN dotnet build "WebSecurityMiddleware.SampleWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebSecurityMiddleware.SampleWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSecurityMiddleware.SampleWebApi.dll"]

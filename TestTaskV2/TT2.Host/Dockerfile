FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TestTaskV2/TestTaskV2.csproj", "TestTaskV2/"]
RUN dotnet restore "TestTaskV2/TestTaskV2.csproj"
COPY . .
WORKDIR "/src/TestTaskV2"
RUN dotnet build "TestTaskV2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTaskV2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTaskV2.dll"]

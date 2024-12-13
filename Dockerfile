FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Messenger/Messenger.csproj", "Messenger/"]
RUN dotnet restore 'Messenger/Messenger.csproj'

COPY ["Messenger", "Messenger/"]
RUN dotnet build 'Messenger/Messenger.csproj' -c Release -o /app/build

FROM build as publish
RUN dotnet publish 'Messenger/Messenger.csproj' -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
ENV ASPNETCORE_HTTP_PORT=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messenger.dll"]
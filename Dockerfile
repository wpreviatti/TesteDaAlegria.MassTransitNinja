FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TesteDaAlegria.MassTransitNinja.csproj", "TesteDaAlegria.MassTransitNinja/"]
RUN dotnet restore "TesteDaAlegria.MassTransitNinja/TesteDaAlegria.MassTransitNinja.csproj"

COPY . TesteDaAlegria.MassTransitNinja/.
RUN dotnet publish -c Release -o /app TesteDaAlegria.MassTransitNinja/TesteDaAlegria.MassTransitNinja.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS publish
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "TesteDaAlegria.MassTransitNinja.dll"]
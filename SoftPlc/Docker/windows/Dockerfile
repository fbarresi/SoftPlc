FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
VOLUME C:\\data
ENV DATA_PATH=C:\\data
EXPOSE 102
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "SoftPlc.csproj"
COPY . .
RUN dotnet build "SoftPlc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoftPlc.csproj" -c Release -o /app/publish
COPY ["/native/win/snap7.dll", "/app/publish/"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["/demodata/datablocks.json", "/demodata/"]
ENTRYPOINT ["dotnet", "SoftPlc.dll"]

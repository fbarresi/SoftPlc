FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
VOLUME /data
ENV DATA_PATH=/data
EXPOSE 102
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "SoftPlc.csproj"
COPY . .
WORKDIR "/src/SoftPlc"
RUN dotnet build "SoftPlc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoftPlc.csproj" -c Release -o /app/publish
COPY ["/native/linux/libsnap7.so", "/app/publish/"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoftPlc.dll"]

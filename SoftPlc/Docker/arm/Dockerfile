FROM microsoft/dotnet:2.1-aspnetcore-runtime-stretch-slim-arm32v7

ARG EXE_DIR=.

WORKDIR /app

COPY $EXE_DIR ./

CMD ["dotnet", "SoftPlc.dll"]

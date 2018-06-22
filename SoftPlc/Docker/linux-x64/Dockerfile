FROM microsoft/dotnet:2.1.1-aspnetcore-runtime-stretch-slim

ARG EXE_DIR=.

WORKDIR /app

COPY $EXE_DIR/release ./

CMD ["ls"]
CMD ["dotnet", "SoftPlc.dll"]
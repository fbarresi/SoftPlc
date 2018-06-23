FROM microsoft/dotnet:2.1.1-aspnetcore-runtime-nanoserver-1803

ARG EXE_DIR=.

WORKDIR /app

COPY $EXE_DIR ./

CMD ["dotnet", "SoftPlc.dll"]

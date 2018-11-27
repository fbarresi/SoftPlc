FROM microsoft/dotnet:2.1-aspnetcore-runtime

ARG EXE_DIR=.

WORKDIR /app

COPY $EXE_DIR ./

VOLUME C:\\data

ENV DATA_PATH=C:\\data

CMD ["dotnet", "SoftPlc.dll"]

EXPOSE 80/tcp

EXPOSE 102/tcp

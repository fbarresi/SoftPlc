FROM microsoft/dotnet:2.1-aspnetcore-runtime-stretch-slim-arm32v7

ARG EXE_DIR=.

WORKDIR /app

COPY $EXE_DIR ./

VOLUME /data

ENV DATA_PATH=/data

CMD ["dotnet", "SoftPlc.dll"]

EXPOSE 80/tcp

EXPOSE 102/tcp

dotnet publish -c release /p:DefineConstants=__MonoCS__ -o release_linux

docker build -f .\Docker\linux-x64\Dockerfile -t fbarresi/softplc:latest-linux .

#docker run -p 5000:80 -p 102:102 --name softplc fbarresi/softplc:latest

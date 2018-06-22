dotnet publish -c release -o release

docker build -f .\Docker\linux-x64\Dockerfile -t fbarresi/softplc:latest .

#docker run -p 5000:80 --name softplc fbarresi/softplc:latest
#dotnet publish -c release -o release-win

docker build -f .\Docker\windows\Dockerfile -t fbarresi/softplc:latest .

dotnet publish -c release -o release-win

docker build -f .\Docker\windows-nano\Dockerfile -t fbarresi/softplc:latest .

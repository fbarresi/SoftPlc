dotnet publish -c release -o release

docker build -f .\Docker\windows-nano\Dockerfile -t fbarresi/softplc:latest .
# SoftPlc
Software PLC controlled over Web API

How hoften did you needed a PLC to test somthing, but you had none?
This project aim to end you pain with test against PLC!

## How does it works

### start from source code 
Checkout and start the software (an ASP.NET core 2 application)

´´´shell
cd SoftPlc
dotnet restore
dotnet build
dotnet bin\Debug\netcoreapp2.0\SoftPlc.dll
´´´

### start with docker
Pull the actual docker image for your platform [see available tags](https://hub.docker.com/r/fbarresi/softplc/tags/) and run it with the correct port binding

´´´
docker pull fbarresi/softplc:1.0-linux
docker run -p 8080:80 -p 102:102 --name softplc fbarresi/softplc:1.0-linux
´´´


Now you have: 
..* an API listening at http://localhost:8080/ ´(that you can control with Swagger visiting http://localhost:8080/swagger )´
..* a Simulated PLC listening at port 102 [see ISO-over-TCP protocol](https://tools.ietf.org/html/rfc1006)

![SoftPlc API](https://github.com/fbarresi/SoftPlc/raw/master/img/SoftPlc_API.png "Api")

## Contribute

Whould you like to contribute? YES, Please! Just fork/star/watch this repo and submit a pull request!

## Credits

This software uses Snap7 as PLC server implementation.
For info please visit the official page: [http://snap7.sourceforge.net](http://snap7.sourceforge.net)
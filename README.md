# SoftPlc
Software PLC controlled over Web API

**How often did you needed a PLC to test somthing, but you had none?**

This project aim to **end you pain** with test against PLC!

## How does it works

### Use it from source code 
Checkout and start the software (an ASP.NET core 2 application)

```shell
cd SoftPlc
dotnet restore
dotnet build
dotnet bin\Debug\netcoreapp2.0\SoftPlc.dll
```

### Use it with docker
Pull the actual docker image for your platform [see available tags](https://hub.docker.com/r/fbarresi/softplc/tags/) and run it with the correct port binding

```docker
docker pull fbarresi/softplc:1.0-linux
docker run -p 8080:80 -p 102:102 --name softplc fbarresi/softplc:1.0-linux
```

Now you have:

- a Simulated PLC listening at port 102 ([see ISO-over-TCP protocol](https://tools.ietf.org/html/rfc1006))

- an API listening at http://localhost:8080/  (with Swagger included under http://localhost:8080/swagger ) in which you can __add__, __read__, __modify__ and __delete__ as many datablocks as you want


![SoftPlc API](https://github.com/fbarresi/SoftPlc/raw/master/img/SoftPlc_API.png "Api")


### Do you also need a PLC Client in your application?

Check my other repository [Sharp7](https://github.com/fbarresi/Sharp7)


## Contribute

Whould you like to contribute? YES, Please! Just fork/star/watch this repo and submit a pull request!

## Credits

This software uses Snap7 as PLC server implementation.
For info please visit the official page: [http://snap7.sourceforge.net](http://snap7.sourceforge.net)

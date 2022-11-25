# SoftPlc [![Docker Image CI](https://github.com/fbarresi/SoftPlc/actions/workflows/docker-images.yml/badge.svg)](https://github.com/fbarresi/SoftPlc/actions/workflows/docker-images.yml) [![Docker Pulls](https://img.shields.io/docker/pulls/fbarresi/softplc.svg)](https://hub.docker.com/r/fbarresi/softplc/)
Software PLC controlled over Web API

**How often did you need a PLC for testing, but you had none?**

This project aim to **end you pain** with test against PLC!

## How does it works

### Use it from source code 
Build and Start the software (don't forget to copy the native library you need)

```shell
cd SoftPlc
dotnet restore
dotnet build
cp native\win\snap7.dll bin\x64\Debug\net6.0\snap7.dll
dotnet bin\Debug\netcoreapp2.0\SoftPlc.dll --plcPort=102 --urls="http://localhost:8080/"
```

### Use it with docker
Pull the actual docker image for your platform [see available tags](https://hub.docker.com/r/fbarresi/softplc/tags/) and run it with the correct port binding. (Brand new MOBY support is included! Just select latest-win1809 tag.)

```docker
docker run -p 8080:80 -p 8443:443 -p 102:102 --name softplc fbarresi/softplc:latest-linux
```

Now you have:

- a Simulated PLC listening at port 102 ([see ISO-over-TCP protocol](https://tools.ietf.org/html/rfc1006))
  - by default, it starts without any data. If you need some preset data you can add `-e DATA_PATH=/demodata` to start with a single 2048 byte DataBlock. The data content is described [here](SoftPlc/demodata/README.md).

- an API listening at http://localhost:8080/  (with Swagger included under http://localhost:8080/ ) in which you can __add__, __read__, __modify__ and __delete__ as many datablocks as you want


![SoftPlc API](https://github.com/fbarresi/SoftPlc/raw/master/img/SoftPlc_API.png "Api")


### Do you also need a PLC Client in your application?

Check my other repository [Sharp7](https://github.com/fbarresi/Sharp7) or [Sharp7Reactive](https://github.com/evopro-ag/Sharp7Reactive)


## Contribute

Whould you like to contribute? YES, Please! Just fork/star/watch this repo and submit a pull request!

## Credits

This software uses Snap7 as PLC server implementation.
For info please visit the official page: [http://snap7.sourceforge.net](http://snap7.sourceforge.net)

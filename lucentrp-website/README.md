# LucentRP Website Server
## Introduction
This directory contains all of the files necessary for building and deploying the server.

## Enironment variables
By default appsettings.json configuration values will be used. These values can be overridden with the following envioronment variables:

- **ALLOWED_HOSTS:** Setting this environment variable will override the **AllowedHosts** value.
- **CONNECTION_STRING:** Setting this environment variable will override the **ConnectionStrings:Default** value.
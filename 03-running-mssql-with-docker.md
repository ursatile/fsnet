---
title: "3: Running MSSQL on Docker"
layout: module
nav_order: 03
typora-copy-images-to: ./assets/images
summary: >
    In this section, we'll use Docker to create a local instance of MS SQL Server and create an empty database to use with our .NET application.
---
## Running SQL Server using Docker

Docker is a platform for running virtualised applications. We're going to use Docker to create and run a minimal virtual machine - known as a **container** - that will host a local version of Microsoft SQL Server.

{: .note }
You'll need Docker Desktop installed to use the examples in this section. You could also connect to a regular database running on a local or remote installation of Microsoft SQL Server, but the instructions in this handbook assume you're running Docker.

To download and the latest SQL Server 2022 image from Docker:

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=p@ssw0rd" -p 1433:1433 --name mssql2022 -d mcr.microsoft.com/mssql/server:2022-latest
```

This will pull  the latest SQL Server 2022 image from Microsoft's Docker image repo, and start a new instance:

* `-e "ACCEPT_EULA=Y"` will automatically accept the End User License Agreement (required to run SQL Server)
* `-e "SA_PASSWORD=p@ssw0rd"` will set the `sa` password to `p@ssw0rd`
* `-p 1433:1433` will map port 1433 on `localhost` to port 1433 on the Docker host. 1433 is the default network port used by SQL Server.
* `--name mssql2022` assigns a name to our host, which we'll use in the next step to run SQL commands on that host.

Use `docker container list` to see a list of running containers and check that our instance has started correctly:

```bash
D:\>docker container list
CONTAINER ID   IMAGE                                        COMMAND                  CREATED         STATUS         PORTS                    NAMES
dbc13666dc93   mcr.microsoft.com/mssql/server:2022-latest   "/opt/mssql/bin/perm…"   5 minutes ago   Up 5 minutes   0.0.0.0:1433->1433/tcp   mssql2022
```

Next, we'll run a SQL script to create a user, set up an empty database, and add our new user to the `db_owner` role in that database. The script is available at [create-tikitapp-database.sql](dotnet/module03/create-tikitapp-database.sql):

```sql
-- create-tikitapp-database.sql

{% include_relative dotnet/module03/create-tikitapp-database.sql %}
```

To copy the script into our Docker container and run it:

```bash
docker cp create-tikitapp-database.sql mssql2022:/opt/create-tikitapp-database.sql
docker exec -it mssql2022 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P p@ssw0rd -i /opt/create-tikitapp-database.sql
```

You should get a response something like:

```
Microsoft SQL Server 2022 (RTM) - 16.0.1000.6 (X64)
        Oct  8 2022 05:58:25
        Copyright (C) 2022 Microsoft Corporation
        Developer Edition (64-bit) on Linux (Ubuntu 20.04.5 LTS) <X64>
Changed database context to 'tikitapp'.
Adding user [tikitapp_user] to database [tikitapp]
Done.
Adding user [tikitapp_user] to role [db_owner] in [tikitapp] database
Done
```

{: .warning }

> If you're using an Apple Mac with the new Apple Silicon M1 or M2 processors, none of this will work, because there isn't an officially supported SQL Server image for the ARM64 architecture used in the M1/M2 Macs. 
>
> You can run the [Azure SQL Edge](https://hub.docker.com/_/microsoft-azure-sql-edge) Docker image on ARM64 Macs using this Docker command:
>
> ```bash
> docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=p@ssw0rd' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge
> ```
>
> That will give you a SQL database instance, but the ARM64 version of SQL Edge doesn't include the `sqlcmd` tool -- so even if you can get it to start, you'll need to connect from a tool like DataGrip and run the SQL script to create the database manually.
>
> Yay progress! 🙃


#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

ARG Default_Port=80
ENV PORT=Default_Port #using of env var with default value
EXPOSE $PORT

#VOLUME ["/app/folder"] creates anonymous volume on a host machine and mapped into container this path;
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Weather.API/Weather.API.csproj", "Weather.API/"]
RUN dotnet restore "Weather.API/Weather.API.csproj"    #command uses NuGet to restore dependencies as well as project-specific tools that are specified in the project file.
COPY . .
WORKDIR "/src/Weather.API"
RUN dotnet build "Weather.API.csproj" -c Release -o /app/build  #Builds a project and all of its dependencies.

FROM build AS publish
RUN dotnet publish "Weather.API.csproj" -c Release -o /app/publish #Publishes the application and its dependencies to a folder for deployment to a hosting system

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weather.API.dll"] #Runs when creates instance of image tells Docker to configure the container to run as an executable.

#CMD ["bash", "-c", "apt update && apt install curl -y"]
RUN bash -c "apt update && apt install curl -y"
CMD ["echo", "running container..."]
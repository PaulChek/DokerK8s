#multy staged dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app


ARG D_PORT=80
ENV "ConnectionStrings:Host"=Succerr 
ENV ASPNETCORE_ENVIRONMENT=Development
ENV PORT $D_PORT 
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
COPY Weather.API/Db Db/
#add my own folder
ENTRYPOINT ["dotnet", "Weather.API.dll"] #Runs when creates instance of image tells Docker to configure the container to run as an executable.

#CMD ["bash", "-c", "apt update && apt install curl -y"]
#RUN bash -c "apt update && apt install curl -y"
#CMD ["echo", "running container..."]
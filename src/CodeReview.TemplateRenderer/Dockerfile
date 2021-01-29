#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["ReviewItEasy.TemplateRenderer/*.csproj", "ReviewItEasy.TemplateRenderer/"]
RUN dotnet restore "ReviewItEasy.TemplateRenderer/ReviewItEasy.TemplateRenderer.csproj" --force

COPY ReviewItEasy.TemplateRenderer/. ./ReviewItEasy.TemplateRenderer/
RUN dotnet publish ./ReviewItEasy.TemplateRenderer/ReviewItEasy.TemplateRenderer.csproj -c Release -o ./ReviewItEasy.TemplateRenderer/out

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS runtime

RUN apt-get update
RUN apt-get --yes install nuget

VOLUME /app/Dictionaries
WORKDIR /app
COPY --from=build /app/ReviewItEasy.TemplateRenderer/out .
ENTRYPOINT ["dotnet", "ReviewItEasy.TemplateRenderer.dll"]
CMD ["--help"]
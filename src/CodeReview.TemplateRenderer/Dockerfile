#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /app
COPY ["CodeReview.TemplateRenderer/*.csproj", "CodeReview.TemplateRenderer/"]
RUN dotnet restore "CodeReview.TemplateRenderer/CodeReview.TemplateRenderer.csproj" --force

COPY CodeReview.TemplateRenderer/. ./CodeReview.TemplateRenderer/
RUN dotnet publish ./CodeReview.TemplateRenderer/CodeReview.TemplateRenderer.csproj -c Release -o ./CodeReview.TemplateRenderer/out

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS runtime

RUN apt-get update
RUN apt-get --yes install nuget

VOLUME /app/Dictionaries
WORKDIR /app
COPY --from=build /app/CodeReview.TemplateRenderer/out .
ENTRYPOINT ["dotnet", "CodeReview.TemplateRenderer.dll"]
CMD ["--help"]
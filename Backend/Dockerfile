FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Checkpromise.csproj ./
RUN dotnet restore ./Checkpromise.csproj
COPY . .
WORKDIR /src/.
RUN dotnet build Checkpromise.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Checkpromise.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Checkpromise.dll"]

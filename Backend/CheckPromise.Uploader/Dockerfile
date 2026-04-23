FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Directory.Build.props Directory.Packages.props ./
COPY CheckPromise.Data/ CheckPromise.Data/
COPY CheckPromise.DTO/ CheckPromise.DTO/
COPY CheckPromise.BusinessLayer/ CheckPromise.BusinessLayer/
COPY CheckPromise.Ingestion/ CheckPromise.Ingestion/
COPY Checkpromise.Provider/ Checkpromise.Provider/
COPY CheckPromise.Uploader/ CheckPromise.Uploader/

RUN dotnet restore CheckPromise.Uploader/CheckPromise.Uploader.csproj
RUN dotnet publish CheckPromise.Uploader/CheckPromise.Uploader.csproj \
    -c Release \
    -o /app \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app

RUN groupadd --system --gid 1000 app \
    && useradd --system --uid 1000 --gid app --create-home app

COPY --from=build --chown=app:app /app ./

RUN mkdir -p /app/data && chown -R app:app /app/data
VOLUME /app/data

USER app

ENV DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CheckPromise.Uploader.dll"]

FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm

# Set up the WASM to run in the container
COPY ./../Imagile.Ui.Web/Imagile.Ui.Web.csproj ./Imagile.Ui.Web/Imagile.Ui.Web.csproj
WORKDIR /Imagile.Ui.Web
RUN dotnet workload restore Imagile.Ui.Web.csproj
RUN dotnet restore Imagile.Ui.Web.csproj

# ENTRYPOINT ["dotnet", "run", "--project", "**/Imagile.Ui.Web.csproj", "--no-restore"]
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN apt-get update
RUN apt-get install --no-install-recommends --yes python3
RUN dotnet workload install wasm-tools
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build-env /app/out/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
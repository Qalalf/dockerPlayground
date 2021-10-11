FROM mcr.microsoft.com/dotnet/sdk
RUN apt-get update && apt-get install vim -y && dotnet dev-certs https
EXPOSE 5000
COPY api/. ./api/
WORKDIR /api
ENTRYPOINT ["dotnet", "run"]


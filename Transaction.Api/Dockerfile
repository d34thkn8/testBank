FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY ./publish/ /app
#ENV ASPNETCORE_URLS=http://+:8080
#ENV ASPNETCORE_ENVIRONMENT De
EXPOSE 6003
ENV ASPNETCORE_URLS=http://+:6003


ENTRYPOINT ["dotnet" , "/app/Transaction.Api.dll"]

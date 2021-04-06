FROM mcr.microsoft.com/dotnet/core/aspnet:5.0 AS runtime
WORKDIR /app
EXPOSE 10000
COPY . /app
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet","Microfisher.Snowflake.Web.dll"]
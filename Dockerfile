FROM mcr.microsoft.com/dotnet/core/sdk:2.2-bionic AS buildDomain

WORKDIR /app

COPY PizzaBox.Domain/ ../PizzaBox.Domain
RUN dotnet restore ../PizzaBox.Domain/*.csproj --no-dependencies
RUN dotnet build ../PizzaBox.Domain/*.csproj --no-restore -c Release
RUN ls ../app
FROM mcr.microsoft.com/dotnet/core/sdk:2.2-bionic AS buildData

WORKDIR /app

COPY PizzaBox.Data/ ../PizzaBox.Data  
COPY --from=buildDomain ./PizzaBox.Domain/ ../PizzaBox.Domain
RUN dotnet restore ../PizzaBox.Data/*.csproj --no-dependencies
RUN dotnet build ../PizzaBox.Data/*.csproj --no-restore -c Release

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-bionic AS buildWeb

WORKDIR /app

COPY PizzaBox.Web/ ../PizzaBox.Web
COPY --from=buildDomain ./PizzaBox.Domain/ ../PizzaBox.Domain
COPY --from=buildData ./PizzaBox.Data/ ../PizzaBox.Data
RUN dotnet restore ../PizzaBox.Web/*.csproj --no-dependencies
RUN dotnet publish ../PizzaBox.Web/*.csproj --no-build -c Release -o out
RUN dotnet build ../PizzaBox.Web/PizzaBox.Web.csproj --no-restore -c Release

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-bionic AS deploy

WORKDIR /app

EXPOSE 80
EXPOSE 443
#RUN dotnet dev-certs https 
#-ep ../PizzaBox.Domain/out/cert.pfx -p test
COPY --from=buildWeb PizzaBox.Web/out ./
ENV ASPNETCORE_URLS="https://+:443"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="crypticpassword"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/aspnetapp.pfx

#CMD ["bash"]
CMD [ "dotnet", "PizzaBox.Web.dll" ]
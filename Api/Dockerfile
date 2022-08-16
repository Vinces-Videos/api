FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Api.csproj"
RUN dotnet publish "Api.csproj" -c Release -o /publish

#Publish can be done here too
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS release
WORKDIR /app
COPY --from=build /publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Api.dll"]
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["TestTask.MaternityHospital.sln", "./"]
COPY ["Libs/", "Libs/"]
COPY ["TestTask.MaternityHospital.App/", "TestTask.MaternityHospital.App/"]

RUN dotnet restore "TestTask.MaternityHospital.sln"

WORKDIR "/src/TestTask.MaternityHospital.App"
RUN dotnet build "TestTask.MaternityHospital.App.csproj" -c Release -o /app/build
RUN dotnet publish "TestTask.MaternityHospital.App.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

RUN apt-get update && apt-get install -y netcat && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish /app

COPY .docker/wait-for-mysql.sh ./wait-for-mysql.sh
RUN chmod +x ./wait-for-mysql.sh

ENTRYPOINT ["./wait-for-mysql.sh", "db", "3306", "dotnet", "TestTask.MaternityHospital.App.dll"]

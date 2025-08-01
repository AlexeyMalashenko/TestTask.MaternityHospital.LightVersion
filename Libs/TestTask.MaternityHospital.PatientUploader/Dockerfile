FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY TestTask.MaternityHospital.PatientUploader.csproj ./
RUN dotnet restore TestTask.MaternityHospital.PatientUploader.csproj

COPY . ./
RUN dotnet publish TestTask.MaternityHospital.PatientUploader.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TestTask.MaternityHospital.PatientUploader.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Feature.API/Feature.API.csproj", "Feature.API/"]
COPY ["Feature.AWS.Storage/Feature.AWS.Storage.csproj", "Feature.AWS.Storage/"]
COPY ["Feature.BusinessModel/Feature.BusinessModel.csproj", "Feature.BusinessModel/"]
COPY ["Feature.Entity/Feature.Entity.csproj", "Feature.Entity/"]
COPY ["Feature.AzureAD/Feature.AzureAD.csproj", "Feature.AzureAD/"]
COPY ["Feature.JWT/Feature.JWT.csproj", "Feature.JWT/"]
COPY ["Feature.Mapper/Feature.Mapper.csproj", "Feature.Mapper/"]
COPY ["Feature.Repositories.ADONET/Feature.ADONET.DAL.csproj", "Feature.Repositories.ADONET/"]
COPY ["Feature.Repository.CodeFirst/Feature.Repository.CodeFirst.csproj", "Feature.Repository.CodeFirst/"]
COPY ["Feature.Repository.Interface/Feature.Repository.Interface.csproj", "Feature.Repository.Interface/"]
COPY ["Feature.Azure.Storage/Feature.Azure.Storage.csproj", "Feature.Azure.Storage/"]
COPY ["Feature.Repository.Dapper/Feature.Repository.Dapper.csproj", "Feature.Repository.Dapper/"]
COPY ["Feature.Repository.DBFirst/Feature.Repository.DBFirst.csproj", "Feature.Repository.DBFirst/"]
COPY ["Feature.Services/Feature.Services.csproj", "Feature.Services/"]
COPY ["Feature.DAL.Repositories/Feature.DAL.Repositories.csproj", "Feature.DAL.Repositories/"]
RUN dotnet restore "Feature.API/Feature.API.csproj"
COPY . .
WORKDIR "/src/Feature.API"
RUN dotnet build "Feature.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Feature.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Feature.API.dll"]
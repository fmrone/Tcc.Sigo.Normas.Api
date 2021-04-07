#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

COPY ["Source/Tcc.Sigo.Normas.Api/Tcc.Sigo.Normas.Api.csproj", "Tcc.Sigo.Normas.Api/"]
COPY ["Source/Tcc.Sigo.Normas.CrossCutting/Tcc.Sigo.Normas.CrossCutting.csproj", "Tcc.Sigo.Normas.CrossCutting/"]
COPY ["Source/Tcc.Sigo.Normas.Repository/Tcc.Sigo.Normas.Repository.csproj", "Tcc.Sigo.Normas.Repository/"]
COPY ["Source/Tcc.Sigo.Normas.Domain/Tcc.Sigo.Normas.Domain.csproj", "Tcc.Sigo.Normas.Domain/"]
COPY ["Source/Tcc.Sigo.Normas.MomAdapter/Tcc.Sigo.Normas.MomAdapter.csproj", "Tcc.Sigo.Normas.MomAdapter/"]
COPY ["Source/Tcc.Sigo.Normas.Application/Tcc.Sigo.Normas.Application.csproj", "Tcc.Sigo.Normas.Application/"]
COPY ["Source/Tcc.Sigo.Normas.Test/Tcc.Sigo.Normas.Test.csproj", "Tcc.Sigo.Normas.Test/"]

COPY . ./
RUN dotnet restore "Source/Tcc.Sigo.Normas.Api/Tcc.Sigo.Normas.Api.csproj"
RUN dotnet test "Source/Tcc.Sigo.Normas.Test/Tcc.Sigo.Normas.Test.csproj"
RUN dotnet publish "Source/Tcc.Sigo.Normas.Api" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Tcc.Sigo.Normas.Api.dll"]
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
ENV TZ=Asia/Shanghai
WORKDIR /app
EXPOSE 80
# 安装libgdiplus库，用于Excel导出
RUN apt-get update && apt-get install -y libgdiplus

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/web/Admin.Web.Host/Magicodes.Admin.Web.Host.csproj", "src/web/Admin.Web.Host/"]
COPY ["src/application/Admin.Application/Magicodes.Admin.Application.csproj", "src/application/Admin.Application/"]
COPY ["src/application/Magicodes.Admin.Application.Core/Magicodes.Admin.Application.Core.csproj", "src/application/Magicodes.Admin.Application.Core/"]
COPY ["src/data/Admin.EntityFrameworkCore/Magicodes.Admin.EntityFrameworkCore.csproj", "src/data/Admin.EntityFrameworkCore/"]
COPY ["src/core/Admin.Core.Custom/Magicodes.Admin.Core.Custom.csproj", "src/core/Admin.Core.Custom/"]
COPY ["src/core/Admin.Core/Magicodes.Admin.Core.csproj", "src/core/Admin.Core/"]
COPY ["src/core/Admin.Localization/Magicodes.Admin.Localization.csproj", "src/core/Admin.Localization/"]
COPY ["src/unity/Admin.Unity/Magicodes.Admin.Unity.csproj", "src/unity/Admin.Unity/"]
COPY ["src/web/Admin.Web.Core/Magicodes.Admin.Web.Core.csproj", "src/web/Admin.Web.Core/"]
COPY ["src/application/Admin.Application.Custom/Magicodes.Admin.Application.Custom.csproj", "src/application/Admin.Application.Custom/"]
RUN dotnet restore "src/web/Admin.Web.Host/Magicodes.Admin.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/web/Admin.Web.Host"
RUN dotnet build "Magicodes.Admin.Web.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Magicodes.Admin.Web.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Magicodes.Admin.Web.Host.dll"]
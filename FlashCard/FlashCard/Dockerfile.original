FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["FlashCard/FlashCard.csproj", "FlashCard/"]
RUN dotnet restore "FlashCard/FlashCard.csproj"
COPY . .
WORKDIR "/src/FlashCard"
RUN dotnet build "FlashCard.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FlashCard.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FlashCard.dll"]
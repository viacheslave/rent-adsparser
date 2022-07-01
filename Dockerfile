FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

COPY ./src/RentAds.Parser/RentAds.Parser.csproj \
      /usr/src/app/src/RentAds.Parser/RentAds.Parser.csproj

COPY ./src/RentAds.Parser.Console/RentAds.Parser.Console.csproj \
      /usr/src/app/src/RentAds.Parser.Console/RentAds.Parser.Console.csproj

COPY ./src/RentAds.Parser.Dal/RentAds.Parser.Dal.csproj \
      /usr/src/app/src/RentAds.Parser.Dal/RentAds.Parser.Dal.csproj

COPY ./src/RentAds.Parser.Jobs/RentAds.Parser.Jobs.csproj \
      /usr/src/app/src/RentAds.Parser.Jobs/RentAds.Parser.Jobs.csproj

COPY ./src/RentAds.Parser.Telegram/RentAds.Parser.Telegram.csproj \
      /usr/src/app/src/RentAds.Parser.Telegram/RentAds.Parser.Telegram.csproj

COPY ./tests/RentAds.Parser.Tests/RentAds.Parser.Tests.csproj \
      /usr/src/app/tests/RentAds.Parser.Tests/RentAds.Parser.Tests.csproj

COPY ./RentAds.Parser.sln /usr/src/app/RentAds.Parser.sln

WORKDIR /usr/src/app
RUN dotnet restore

COPY . /usr/src/app
RUN dotnet build -c Release
RUN dotnet publish -o "artifact" -c Release -r ubuntu.22.04-x64 --self-contained false "src/RentAds.Parser.Console/RentAds.Parser.Console.csproj"

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as release
WORKDIR /usr/src/app
COPY --from=build /usr/src/app/artifact .
EXPOSE 80
CMD [ "./RentAds.Parser.Console" ]
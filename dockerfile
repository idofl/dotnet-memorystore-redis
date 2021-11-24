FROM mcr.microsoft.com/dotnet/runtime:5.0

COPY Redistest/bin/Release/net5.0/publish/ App/
COPY server-ca.pem /usr/local/share/ca-certificates/server-ca.crt
WORKDIR /App

RUN update-ca-certificates

ENTRYPOINT ["dotnet", "Redistest.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1

ARG SCANNER_IP
ARG SCANNER_MODEL
ARG FRIENDLY_NAME

# Add nodejs from nodesource and yarn
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add -
RUN echo 'deb https://dl.yarnpkg.com/debian/ stable main' | tee /etc/apt/sources.list.d/yarn.list

RUN apt-get update && apt-get install -y \
    nodejs \
    yarn \
    sqlite3 \
    sane-utils \
    imagemagick
 
WORKDIR /app

# Install Brother scanner driver tool
COPY driver/brscan4-0.4.8-1.amd64.deb ./brscan4.deb
RUN dpkg -i --force-all brscan4.deb

RUN brsaneconfig4 -a name=$FRIENDLY_NAME model=$SCANNER_MODEL ip=$SCANNER_IP

# Copy && Run the APP
COPY . .
ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

ENTRYPOINT [ "dotnet" , "watch", "run" ]
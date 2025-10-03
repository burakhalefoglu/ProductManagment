### 1. Aşama: Flutter Web Build ###
FROM ghcr.io/cirruslabs/flutter:3.29.1 AS build

WORKDIR /app
COPY . .

# Build arg'ları al (docker-compose üzerinden gelecek)
ARG URL
ARG ENVIRONMENT
ARG FIREBASE

# Bağımlılıkları indir
RUN flutter pub get


# Web için derle
RUN flutter build web \
    --release \
    --dart-define=URL=${URL} \
    --dart-define=ENVIRONMENT=${ENVIRONMENT} \
    --dart-define=FIREBASE=${FIREBASE}

### 2. Aşama: NGINX sunumu ###
FROM nginx:alpine

# Sağlık kontrolü için curl kur
RUN apk add --no-cache curl

# Derlenmiş web dosyalarını kopyala
COPY --from=build /app/build/web /usr/share/nginx/html

# Nginx konfigürasyon dosyası (isteğe bağlı)
COPY default.conf /etc/nginx/conf.d/default.conf

EXPOSE 3000

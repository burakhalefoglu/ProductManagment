# ---------- 1) Build Stage ----------
FROM node:24-alpine AS build
WORKDIR /app

# Stabil indirme (timeout artır)
RUN npm config set fetch-timeout 120000 \
 && npm config set fetch-retry-maxtimeout 120000

# Cache için sadece manifestleri kopyala
COPY package.json package-lock.json ./
RUN npm ci --no-audit --prefer-offline

# Kaynakları kopyala
COPY . .

# API URL — öncelik: --build-arg API_URL > --build-arg URL > default
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}

# Prod env dosyası
RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts \
 && sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Angular prod build (local CLI; package.json -> "build": "ng build")
RUN npm run build -- --configuration production

# ---------- 2) Runtime Stage ----------
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Tüm çıktıyı önce geçici dizine al
COPY --from=build /app/dist/product-dashboard-angular /tmp/app

# Eğer browser klasörü varsa onu; yoksa kökü kopyala
RUN if [ -d /tmp/app/browser ]; then \
      cp -r /tmp/app/browser/* . ; \
    else \
      cp -r /tmp/app/* . ; \
    fi

# Basit SPA nginx conf (opsiyonel ama iyi)
COPY default.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

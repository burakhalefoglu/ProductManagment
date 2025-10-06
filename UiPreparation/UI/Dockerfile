# =========================================
# Stage 1: Build (Node 24 + npm)
# =========================================
ARG NODE_VERSION=24.9.0-alpine
ARG NGINX_VERSION=alpine3.22
FROM node:${NODE_VERSION} AS builder
WORKDIR /app

# Native build araçları + CA
RUN apk add --no-cache python3 make g++ git ca-certificates

# npm daha stabil/temiz çıktılar
RUN npm config set fund false \
 && npm config set audit false \
 && npm config set progress false

# Sadece manifestleri kopyala ve deterministik kurulum
# (package-lock.json şart. Yoksa lokalde `npm i --package-lock-only` ile üret)
COPY package.json package-lock.json ./
RUN npm ci

# Kaynakları kopyala
COPY . .

# API URL (API_URL > URL > default)
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}

# env prod dosyasını şablondan üret
# (projende: src/environments/environment.prod.template.ts içinde __API_URL__ placeholder’ı olmalı)
RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts \
 && sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Angular prod build
# CLI devDependencies'teyse direkt npx ile kullan
RUN npx ng build --configuration production

# =========================================
# Stage 2: Runtime (nginx-unprivileged, 3000)
# =========================================
FROM nginxinc/nginx-unprivileged:${NGINX_VERSION} AS runner

USER root
# SPA fallback + opsiyonel healthz ekleyebilirsin
COPY default.conf /etc/nginx/conf.d/default.conf

# dist/<proj>/browser varsa onu, yoksa kökü kopyala
COPY --from=builder /app/dist /tmp/dist
RUN set -e; \
    mkdir -p /usr/share/nginx/html; \
    if ls /tmp/dist/*/browser 1>/dev/null 2>&1; then \
      cp -r /tmp/dist/*/browser/* /usr/share/nginx/html/; \
    else \
      cp -r /tmp/dist/*/* /usr/share/nginx/html/ 2>/dev/null || cp -r /tmp/dist/* /usr/share/nginx/html/; \
    fi; \
    chown -R 101:101 /usr/share/nginx/html

USER 101
EXPOSE 3000
ENTRYPOINT ["nginx","-g","daemon off;"]

# ============================
# Stage 1: Build (Node 20 + npm10)
# ============================
ARG NODE_VERSION=20-alpine
ARG NGINX_VERSION=alpine3.22
FROM node:${NODE_VERSION} AS builder
WORKDIR /app

# Gerekli araçlar + CA
RUN apk add --no-cache python3 make g++ git ca-certificates

# npm 10'a pinle (npm 11 hatasını atla) + ağ ayarları
RUN npm i -g npm@10.7.0 \
 && npm config set fund false \
 && npm config set audit false \
 && npm config set progress false \
 && npm config set fetch-retries 5 \
 && npm config set fetch-retry-maxtimeout 120000 \
 && npm config set fetch-timeout 120000

# Deterministik kurulum (package-lock.json zorunlu)
COPY package.json package-lock.json ./
RUN --mount=type=cache,target=/root/.npm npm ci --legacy-peer-deps

# Kaynaklar
COPY . .

# API URL (API_URL > URL > default)
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}

# env prod dosyasını üret (template içindeki __API_URL__ yerini doldur)
RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts \
 && sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Angular 20 ile prod build (lokal CLI yoksa 20'yi kullan)
RUN npx -y @angular/cli@20 ng build --configuration production

# ============================
# Stage 2: Runtime (nginx-unprivileged, 3000)
# ============================
FROM nginxinc/nginx-unprivileged:${NGINX_VERSION} AS runner

USER root
COPY default.conf /etc/nginx/conf.d/default.conf

# dist/<proj>/browser varsa onu, yoksa fallback
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

# =========================================
# Stage 1: Build the Angular Application
# =========================================
ARG NODE_VERSION=24.7.0-alpine
ARG NGINX_VERSION=alpine3.22

FROM node:${NODE_VERSION} AS builder
WORKDIR /app

# (Alpine) native modüller gerekirse derleyebilsin
RUN apk add --no-cache python3 make g++ git

# Yalnızca manifestleri kopyala (cache verimi için)
COPY package.json package-lock.json ./

# Dokümantasyon stili: BuildKit cache + npm ci (ekstra bayrak yok)
RUN --mount=type=cache,target=/root/.npm npm ci

# Kaynak kodu kopyala
COPY . .

# (Opsiyonel) API URL enjekte et: API_URL > URL > default
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}

RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts \
 && sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Prod build (package.json -> "build": "ng build")
RUN npm run build -- --configuration production


# =========================================
# Stage 2: Prepare Nginx to Serve Static Files
# =========================================
FROM nginxinc/nginx-unprivileged:${NGINX_VERSION} AS runner

# Config kopyalamak ve dosya izinlerini ayarlamak için root'a geç
USER root
COPY nginx.conf /etc/nginx/nginx.conf

# Build çıktısını al
COPY --from=builder /app/dist /tmp/dist

# Angular sürümüne göre bazen dist/<proj>/browser üretilir.
# Varsa browser'ı, yoksa proje kökünü kopyala.
RUN set -e; \
    mkdir -p /usr/share/nginx/html; \
    if ls /tmp/dist/*/browser 1>/dev/null 2>&1; then \
      cp -r /tmp/dist/*/browser/* /usr/share/nginx/html/; \
    else \
      # dist/<proj> yapısını da kapsa
      if ls /tmp/dist/* 1>/dev/null 2>&1; then \
        cp -r /tmp/dist/*/* /usr/share/nginx/html/ 2>/dev/null || cp -r /tmp/dist/* /usr/share/nginx/html/; \
      fi; \
    fi; \
    chown -R 101:101 /usr/share/nginx/html

# Non-root kullanıcıya dön
USER nginx

# nginx-unprivileged 8080'de dinler
EXPOSE 8080

ENTRYPOINT ["nginx", "-c", "/etc/nginx/nginx.conf"]
CMD ["-g", "daemon off;"]

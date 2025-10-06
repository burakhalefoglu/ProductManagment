# =========================================
# Stage 1: Build the Angular Application
# =========================================
ARG NODE_VERSION=24.9.0-alpine
ARG NGINX_VERSION=alpine3.22

FROM node:${NODE_VERSION} AS builder
WORKDIR /app

# Derleme için gerekli native araçlar (Alpine)
RUN apk add --no-cache python3 make g++ git

# Versiyonları logla (ağ gerektirmez) -> engines doğrulaması
RUN node -v && npm -v

# Sadece manifestleri kopyala (cache verimi)
COPY package.json package-lock.json ./

# Dokümantasyon stili: BuildKit cache + npm ci (EKSTRA BAYRAK YOK)
RUN --mount=type=cache,target=/root/.npm npm ci

# Uygulama kaynakları
COPY . .

# API URL (API_URL > URL > default)
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}

# environment.prod.ts üret
RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts \
 && sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Prod build (package.json -> "build": "ng build")
RUN npm run build -- --configuration production


# =========================================
# Stage 2: Prepare Nginx to Serve Static Files
# =========================================
FROM nginxinc/nginx-unprivileged:${NGINX_VERSION} AS runner

# Non-root kullanıcı (nginx) ile çalışacağız
USER nginx

# Özel nginx.conf varsa kopyala
COPY --chown=nginx:nginx nginx.conf /etc/nginx/nginx.conf

# Angular 17/18'de bazen dist/<proj>/browser oluşur; yoksa kök kopyalanır
# Güvenli kopyalama için önce geçici dizine alıp sonra koşullu kopyala
USER root
COPY --from=builder /app/dist /tmp/dist
RUN set -e; \
    mkdir -p /usr/share/nginx/html; \
    if ls /tmp/dist/*/browser 1>/dev/null 2>&1; then \
      cp -r /tmp/dist/*/browser/* /usr/share/nginx/html/; \
    else \
      cp -r /tmp/dist/*/* /usr/share/nginx/html/ 2>/dev/null || cp -r /tmp/dist/* /usr/share/nginx/html/; \
    fi; \
    chown -R 101:101 /usr/share/nginx/html

# Non-root'a geri dön
USER nginx

# nginx-unprivileged 3000'de dinler
EXPOSE 3000

ENTRYPOINT ["nginx", "-c", "/etc/nginx/nginx.conf"]
CMD ["-g", "daemon off;"]

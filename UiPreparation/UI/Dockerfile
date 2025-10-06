# =========================================
# Stage 1: Build the Angular Application
# =========================================
ARG NODE_VERSION=24.9.0-alpine
ARG NGINX_VERSION=alpine3.22

FROM node:${NODE_VERSION} AS builder
WORKDIR /app

# Native modüller için
RUN apk add --no-cache python3 make g++ git

# npm sürümünü engines ile hizala (11.6.0)
RUN npm i -g npm@11.6.0 && npm -v

# Cache verimi için sadece manifestleri kopyala
COPY package.json package-lock.json ./

# Doküman stili: BuildKit cache + npm ci (ekstra bayrak yok)
RUN --mount=type=cache,target=/root/.npm npm ci

# Kaynak kodu kopyala
COPY . .

# API URL enjekte et (API_URL > URL > default)
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}

RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts \
 && sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Prod build
RUN npm run build -- --configuration production

# =========================================
# Stage 2: Prepare Nginx to Serve Static Files
# =========================================
FROM nginxinc/nginx-unprivileged:${NGINX_VERSION} AS runner

USER root
COPY nginx.conf /etc/nginx/nginx.conf

# Çıktıyı al
COPY --from=builder /app/dist /tmp/dist

# browser varsa onu; yoksa kök dizini kopyala
RUN set -e; \
    mkdir -p /usr/share/nginx/html; \
    if ls /tmp/dist/*/browser 1>/dev/null 2>&1; then \
      cp -r /tmp/dist/*/browser/* /usr/share/nginx/html/; \
    else \
      cp -r /tmp/dist/*/* /usr/share/nginx/html/ 2>/dev/null || cp -r /tmp/dist/* /usr/share/nginx/html/; \
    fi; \
    chown -R 101:101 /usr/share/nginx/html

USER nginx
EXPOSE 3000
ENTRYPOINT ["nginx", "-c", "/etc/nginx/nginx.conf"]
CMD ["-g", "daemon off;"]

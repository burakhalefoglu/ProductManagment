# =========================================
# Stage 1: Build (Node 24 + Yarn Classic)
# =========================================
ARG NODE_VERSION=24.9.0-alpine
ARG NGINX_VERSION=alpine3.22
FROM node:${NODE_VERSION} AS builder
WORKDIR /app

# Native derleme araçları
RUN apk add --no-cache python3 make g++ git

# Manifestleri kopyala
COPY package.json package-lock.json ./

# Yarn Classic kullan (npm 11 bug’ını pas geç)
RUN corepack enable && corepack prepare yarn@1.22.22 --activate
RUN yarn import --silent
RUN --mount=type=cache,target=/usr/local/share/.cache/yarn \
    yarn install --frozen-lockfile

# Kaynakları kopyala
COPY . .

# API URL (API_URL > URL > default)
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}

# env prod
RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts \
 && sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Prod build (package.json -> "build": "ng build")
RUN yarn run build --configuration production

# =========================================
# Stage 2: Runtime (nginx-unprivileged, 3000)
# =========================================
FROM nginxinc/nginx-unprivileged:${NGINX_VERSION} AS runner
USER root
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
USER nginx

EXPOSE 3000
ENTRYPOINT ["nginx","-g","daemon off;"]

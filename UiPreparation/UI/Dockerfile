# ---------- 1) Build Stage ----------
FROM node:24-alpine AS build
WORKDIR /app

# Native modüller için (node-gyp vs.)
RUN apk add --no-cache python3 make g++ git

# Cache verimli kurulum
COPY package.json package-lock.json ./
RUN npm ci --no-audit --prefer-offline

# Kaynakları kopyala
COPY . .

# API URL (öncelik: API_URL > URL > default)
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}

# Prod env
RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts \
 && sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Angular prod build (local CLI: package.json -> "build": "ng build")
RUN npm run build -- --configuration production

# ---------- 2) Runtime Stage ----------
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Build çıktısını al (browser varsa onu, yoksa kökü)
COPY --from=build /app/dist/product-dashboard-angular /tmp/app
RUN if [ -d /tmp/app/browser ]; then cp -r /tmp/app/browser/* .; else cp -r /tmp/app/* .; fi

# SPA fallback için basit config (dosyan varsa kalsın, yoksa nginx default da çalışır)
# COPY default.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

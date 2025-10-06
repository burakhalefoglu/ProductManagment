# ---------- Build stage ----------
ARG NODE_VERSION=22.11.0-alpine
ARG NGINX_VERSION=alpine3.22

FROM node:${NODE_VERSION} AS builder
WORKDIR /app

# Stabil npm
RUN npm install -g @angular/cli 

# 1) Sadece lock ve manifest dosyaları
COPY package.json package-lock.json ./

# 2) Deterministic kurulum
RUN --mount=type=cache,target=/root/.npm npm ci --omit=optional --no-audit --no-fund

# 3) Uygulama kaynakları
COPY . .

# API_URL üret
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}
RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts \
&& sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Angular build
RUN npm run build

FROM nginxinc/nginx-unprivileged:${NGINX_VERSION} AS runner
USER nginx
COPY nginx.conf /etc/nginx/nginx.conf
COPY --chown=nginx:nginx --from=builder /app/dist/*/browser /usr/share/nginx/html
EXPOSE 3000
ENTRYPOINT ["nginx", "-c", "/etc/nginx/nginx.conf"]
CMD ["-g", "daemon off;"]

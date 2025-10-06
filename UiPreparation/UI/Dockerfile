ARG NODE_VERSION=22.11.0-alpine   # 24 yerine 22 LTS
ARG NGINX_VERSION=alpine3.22

FROM node:${NODE_VERSION} AS builder
WORKDIR /app

# NPM'i daha stabil bir sürüme sabitle (npm 10.8.x genelde sorunsuz)
RUN npm i -g npm@10.8.3

COPY package.json package-lock.json ./

# Optional bağımlılıkları atla; audit/fund kapat
RUN --mount=type=cache,target=/root/.npm npm ci --no-audit --no-fund --omit=optional

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

### 1) Angular Build
FROM node:20-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .

# Build arg (opsiyonel). Arg gelmezse varsayılan ver.
ARG URL
ARG API_URL
ENV API_URL_EFFECTIVE=${API_URL:-${URL:-https://localhost:5001/api/v1}}

# sed ile __API_URL__ -> ${API_URL_EFFECTIVE}
RUN cp src/environments/environment.prod.template.ts src/environments/environment.prod.ts && \
    sed -i "s#__API_URL__#${API_URL_EFFECTIVE}#g" src/environments/environment.prod.ts

# Prod build
RUN npx ng build --configuration production

### 2) Nginx Serve
FROM nginx:alpine
RUN apk add --no-cache curl

COPY --from=build /app/dist/product-dashboard-angular/browser /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/default.conf

EXPOSE 3000
CMD ["nginx", "-g", "daemon off;"]

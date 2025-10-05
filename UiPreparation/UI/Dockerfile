FROM node:20-alpine AS build

WORKDIR /app
COPY . .

ARG URL
ENV API_URL=${URL:-https://localhost:5001/api/v1}

RUN npm install -g @angular/cli
RUN npm install

RUN sed -i "s#__API_URL__#${API_URL}#g" src/environments/environment.ts

RUN ng build --configuration production

FROM nginx:alpine
RUN apk add --no-cache curl

COPY --from=build /app/dist /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/default.conf

EXPOSE 3000
CMD ["nginx", "-g", "daemon off;"]

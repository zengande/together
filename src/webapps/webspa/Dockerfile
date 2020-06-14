FROM nginx:stable AS base
WORKDIR /app
EXPOSE 80

FROM node:lts AS build
WORKDIR /src

COPY ["src/webapps/webspa", "."]
RUN npm install
COPY . .

FROM build AS publish
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=publish /src/dist .
COPY ["src/webapps/webspa/nginx.conf", "/etc/nginx/nginx.conf"]
ENTRYPOINT ["nginx","-g","daemon off;"]
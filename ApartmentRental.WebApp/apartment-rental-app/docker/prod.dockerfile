### Stage 1

# Get node image for building purposes
FROM node:10.15.0 as node

# Create a new folder inside image where the app will be built
WORKDIR /app
COPY . .

EXPOSE 4200

RUN npm install

RUN npm i yarn

RUN yarn global add @angular/cli@latest

RUN ng build -prod

### Stage 2

FROM nginx:alpine

COPY --from=node /app/dist/ /usr/share/nginx/html
COPY --from=node /app/docker/nginx.conf /etc/nginx/conf.d/default.conf
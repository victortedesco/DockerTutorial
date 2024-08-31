FROM node:22-alpine AS build

WORKDIR /app

COPY src/web/package*.json ./

RUN npm install -g @angular/cli

RUN npm install

COPY src/web/. .

RUN npm run build

FROM build AS run

RUN apk update && apk add curl

EXPOSE 4000

CMD ["npm", "run", "serve:ssr:web"]
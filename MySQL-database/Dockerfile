FROM node

WORKDIR /app

COPY package.json .

RUN npm install

COPY . .

EXPOSE 9080

CMD ["node", "server.js"]
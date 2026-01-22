FROM node:20-slim
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .

ENV VITE_HOST=0.0.0.0
ENV VITE_PORT=4173
ENV NODE_ENV=production

RUN npm run build

EXPOSE 4173

CMD ["npm", "run", "preview"]
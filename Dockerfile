# Use a stable Node.js LTS version on a lightweight Alpine Linux base
FROM node:20-alpine

# Set the working directory inside the container
WORKDIR /app

# Copy the package files first to leverage Docker's build cache
# This ensures npm install is only re-run if package.json or package-lock.json changes
COPY CSA-Clone-HTML/csa-backend/package*.json ./CSA-Clone-HTML/csa-backend/

# Change to the backend directory and install production dependencies
WORKDIR /app/CSA-Clone-HTML/csa-backend
RUN npm install --production

# Go back to the /app root to copy the rest of the application
WORKDIR /app

# Copy the entire CSA-Clone-HTML directory (which includes frontend assets and backend code)
# Files excluded by .dockerignore (like node_modules and .env) will not be copied
COPY CSA-Clone-HTML/ ./CSA-Clone-HTML/

# Change directory back to the backend where server.js resides
WORKDIR /app/CSA-Clone-HTML/csa-backend

# The server defaults to port 5000 as per server.js
EXPOSE 5000

# Start the Express server using node
CMD ["node", "server.js"]

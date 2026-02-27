# Use Node.js LTS version on Alpine Linux
FROM node:22-alpine

# Install PM2 globally
RUN npm install -g pm2

# Set the working directory
WORKDIR /app

# --- Setup WhatsApp Server ---
# Copy package files and install dependencies
COPY whatsapp-server/package*.json ./whatsapp-server/
RUN cd whatsapp-server && npm install --production

# Copy source files
COPY whatsapp-server/whatsapp-server.js ./whatsapp-server/
COPY whatsapp-server/index.html ./whatsapp-server/

# --- Setup CSA Backend ---
# Copy package files and install dependencies
COPY CSA-Clone-HTML/csa-backend/package*.json ./CSA-Clone-HTML/csa-backend/
RUN cd CSA-Clone-HTML/csa-backend && npm install --production

# Copy entire directory structure (frontend + backend)
COPY CSA-Clone-HTML/ ./CSA-Clone-HTML/

# --- Main configuration ---
# Copy PM2 ecosystem config
COPY ecosystem.config.cjs ./

# Set production environment
ENV NODE_ENV=production
ENV PORT=5000
ENV WHATSAPP_SERVER_PORT=3182

# Expose ports
# csa-backend: 5000
# whatsapp-server: 3182
EXPOSE 5000 3182

# Health check (checks if backend is responding)
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD wget --no-verbose --tries=1 --spider http://localhost:5000/ || exit 1

# Start both services with PM2
CMD ["pm2-runtime", "ecosystem.config.cjs"]

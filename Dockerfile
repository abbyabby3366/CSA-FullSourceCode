# ============================================
# Stage 1: Build & Dependencies
# ============================================
FROM node:22-bookworm-slim AS builder

WORKDIR /app

# --- CSA Backend Dependencies ---
COPY CSA-Clone-HTML/csa-backend/package.json CSA-Clone-HTML/csa-backend/
COPY CSA-Clone-HTML/csa-backend/package-lock.json CSA-Clone-HTML/csa-backend/
RUN cd CSA-Clone-HTML/csa-backend && npm ci --omit=dev

# --- WhatsApp Server Dependencies ---
COPY whatsapp-server/package.json whatsapp-server/
COPY whatsapp-server/package-lock.json whatsapp-server/
RUN cd whatsapp-server && npm ci --omit=dev

# ============================================
# Stage 2: Production runtime
# ============================================
FROM node:22-bookworm-slim AS runner

# --- Install YOUR requested dependencies ---
RUN apt-get update && apt-get install -y \
    wget \
    ca-certificates \
    fonts-liberation \
    libnss3 \
    libatk1.0-0 \
    libatk-bridge2.0-0 \
    libcups2 \
    libdrm2 \
    libxcomposite1 \
    libxdamage1 \
    libxrandr2 \
    libgbm1 \
    libasound2 \
    && rm -rf /var/lib/apt/lists/*

# Install PM2 globally
RUN npm install -g pm2

WORKDIR /app

# (The rest of the COPY commands remain the same as before...)
COPY --from=builder /app/CSA-Clone-HTML/csa-backend/node_modules ./CSA-Clone-HTML/csa-backend/node_modules
COPY --from=builder /app/whatsapp-server/node_modules ./whatsapp-server/node_modules
COPY CSA-Clone-HTML/ ./CSA-Clone-HTML/
COPY whatsapp-server/whatsapp-server.js whatsapp-server/index.html whatsapp-server/package.json ./whatsapp-server/
COPY ecosystem.config.cjs ./

ENV NODE_ENV=production
ENV PORT=5000
ENV WHATSAPP_SERVER_PORT=3182

EXPOSE 5000 3182

CMD ["pm2-runtime", "ecosystem.config.cjs"]

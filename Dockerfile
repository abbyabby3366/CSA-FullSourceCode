# ============================================
# Stage 1: Build & Dependencies
# ============================================
FROM node:22-alpine AS builder

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
FROM node:22-alpine AS runner

# Install PM2 globally for process management
RUN npm install -g pm2

WORKDIR /app

# --- Copy Dependencies from Builder ---
COPY --from=builder /app/CSA-Clone-HTML/csa-backend/node_modules ./CSA-Clone-HTML/csa-backend/node_modules
COPY --from=builder /app/whatsapp-server/node_modules ./whatsapp-server/node_modules

# --- Copy Source Files ---
# CSA Backend + Frontend (HTML files)
COPY CSA-Clone-HTML/ ./CSA-Clone-HTML/

# WhatsApp Server
COPY whatsapp-server/whatsapp-server.js whatsapp-server/
COPY whatsapp-server/index.html whatsapp-server/
COPY whatsapp-server/package.json whatsapp-server/

# PM2 ecosystem config
COPY ecosystem.config.cjs ./

# Environment file template
COPY .env.example ./.env.example 2>/dev/null || true

# ============================================
# Configuration & Environment
# ============================================
ENV NODE_ENV=production
ENV PORT=5000
ENV WHATSAPP_SERVER_PORT=3182
ENV BODY_SIZE_LIMIT=30M

# Expose ports
# Primary backend: 5000
# WhatsApp server: 3182
EXPOSE 5000
EXPOSE 3182

# Health check (checks if main backend is responding)
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD wget --no-verbose --tries=1 --spider http://localhost:5000/health || wget --no-verbose --tries=1 --spider http://localhost:5000/ || exit 1

# Start both services with PM2
CMD ["pm2-runtime", "ecosystem.config.cjs"]


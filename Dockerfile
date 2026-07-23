# ============================================
# Stage 1: Build & Dependencies
# ============================================
FROM node:22-bookworm-slim AS builder

WORKDIR /app

# --- CSA Backend Dependencies ---
COPY CSA-Clone-HTML/csa-backend/package.json CSA-Clone-HTML/csa-backend/
COPY CSA-Clone-HTML/csa-backend/package-lock.json CSA-Clone-HTML/csa-backend/
RUN cd CSA-Clone-HTML/csa-backend && npm ci --omit=dev

# ============================================
# Stage 2: Production runtime
# ============================================
FROM node:22-bookworm-slim AS runner

# Install PM2 globally
RUN npm install -g pm2

WORKDIR /app

COPY CSA-Clone-HTML/ ./CSA-Clone-HTML/
COPY --from=builder /app/CSA-Clone-HTML/csa-backend/node_modules ./CSA-Clone-HTML/csa-backend/node_modules
COPY ecosystem.config.cjs ./

ENV NODE_ENV=production
ENV PORT=5000

EXPOSE 5000

CMD ["pm2-runtime", "ecosystem.config.cjs"]

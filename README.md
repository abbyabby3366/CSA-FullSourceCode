# CSA Project - Run Instructions

This project consists of two main services managed by PM2:

1.  **WhatsApp Server**: Handles message delivery and authentication.
2.  **CSA Backend**: Handles business logic, authentication, and registration.

## Prerequisites

- Node.js (v22+)
- PM2 (`npm install -g pm2`)
- MongoDB (Connection string in `.env`)
- Redis (Required for WhatsApp authentication)

## Option 1: Run Locally with PM2

1.  **Setup Environment**:
    Ensure the `.env` file exists in the root directory. It should contain MongoDB, Redis, and WhatsApp configuration.

2.  **Install Dependencies**:

    ```bash
    # Install backend dependencies
    cd CSA-Clone-HTML/csa-backend && npm install
    # Install WhatsApp server dependencies
    cd ../../whatsapp-server && npm install
    cd ..
    ```

3.  **Start Services**:

    ```bash
    pm2 start ecosystem.config.cjs
    ```

4.  **Monitor**:
    ```bash
    pm2 status
    pm2 logs
    ```

## Option 2: Run with Docker

1.  **Build Image**:

    ```bash
    docker build -t csa-app .
    ```

2.  **Run Container**:
    ```bash
    docker run -p 5000:5000 -p 3182:3182 --env-file .env csa-app
    ```

## WhatsApp Setup

After starting, visit the WhatsApp server logs or status endpoint to scan the QR code:

- Status: `http://localhost:3182/whatsapp-status`
- Scan QR: Authenticate your phone to start sending TAC codes.

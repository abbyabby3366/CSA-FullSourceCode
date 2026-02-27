module.exports = {
  apps: [
    {
      name: "whatsapp-server",
      script: "node",
      cwd: "./whatsapp-server",
      args: "whatsapp-server.js",
      ...(process.env.NODE_ENV === "production" ? {} : { env_file: "../.env" }),
      env: {
        WHATSAPP_SERVER_PORT: process.env.WHATSAPP_SERVER_PORT || 3182,
      },
    },
    {
      name: "csa-backend",
      script: "node",
      args: "server.js",
      cwd: "./CSA-Clone-HTML/csa-backend",
      ...(process.env.NODE_ENV === "production"
        ? {}
        : { env_file: "../../.env" }),
      env: {
        PORT: process.env.PORT || 5000,
      },
    },
  ],
};

module.exports = {
  apps: [
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

const http = require("http");

const testEndpoint = (url) => {
  return new Promise((resolve) => {
    http
      .get(url, (res) => {
        let data = "";
        res.on("data", (chunk) => (data += chunk));
        res.on("end", () => {
          try {
            const json = JSON.parse(data);
            if (json.status === "ok") {
              console.log(`✅ ${url} is working correctly.`);
              resolve(true);
            } else {
              console.log(`❌ ${url} returned unexpected response: ${data}`);
              resolve(false);
            }
          } catch (e) {
            console.log(`❌ ${url} did not return valid JSON: ${data}`);
            resolve(false);
          }
        });
      })
      .on("error", (err) => {
        console.log(
          `⚠️ ${url} could not be reached (Server might not be running): ${err.message}`,
        );
        resolve(false);
      });
  });
};

async function runTests() {
  console.log("Starting verification of /ping endpoints...");
  // Note: These will likely fail if the servers aren't actually running,
  // but they serve as a check for the intended ports.
  await testEndpoint("http://localhost:5000/ping");
  await testEndpoint("http://localhost:3182/ping");
}

runTests();

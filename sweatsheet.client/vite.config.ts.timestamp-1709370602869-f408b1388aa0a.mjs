// vite.config.ts
import { defineConfig } from "file:///C:/Users/hango/OneDrive/Desktop/SweatSheet/sweatsheet.client/node_modules/vite/dist/node/index.js";
import plugin from "file:///C:/Users/hango/OneDrive/Desktop/SweatSheet/sweatsheet.client/node_modules/@vitejs/plugin-vue/dist/index.mjs";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";
import Components from "file:///C:/Users/hango/OneDrive/Desktop/SweatSheet/sweatsheet.client/node_modules/unplugin-vue-components/dist/vite.js";
import { VantResolver } from "file:///C:/Users/hango/OneDrive/Desktop/SweatSheet/sweatsheet.client/node_modules/@vant/auto-import-resolver/dist/index.esm.mjs";
var __vite_injected_original_dirname = "C:\\Users\\hango\\OneDrive\\Desktop\\SweatSheet\\sweatsheet.client";
var baseFolder = env.APPDATA !== void 0 && env.APPDATA !== "" ? `${env.APPDATA}/ASP.NET/https` : `${env.HOME}/.aspnet/https`;
var certificateName = "sweatsheet.client";
var certFilePath = path.join(baseFolder, `${certificateName}.pem`);
var keyFilePath = path.join(baseFolder, `${certificateName}.key`);
if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  if (0 !== child_process.spawnSync(
    "dotnet",
    ["dev-certs", "https", "--export-path", certFilePath, "--format", "Pem", "--no-password"],
    { stdio: "inherit" }
  ).status) {
    throw new Error("Could not create certificate.");
  }
}
var target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` : env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(";")[0] : "https://localhost:7038";
var vite_config_default = defineConfig({
  plugins: [
    plugin(),
    // VueDevTools(),
    Components({
      resolvers: [VantResolver()]
    })
  ],
  resolve: {
    alias: {
      "@workouts": path.resolve(`${path.resolve(__vite_injected_original_dirname, "./src/modules/workouts")}`),
      "@exercises": path.resolve(`${path.resolve(__vite_injected_original_dirname, "./src/modules/exercises")}`),
      "@shared": path.resolve(`${path.resolve(__vite_injected_original_dirname, "./src/modules/shared")}`),
      "@": path.resolve(__vite_injected_original_dirname, "./src")
    }
  },
  server: {
    proxy: {
      "^/api": {
        target,
        secure: false
      }
    },
    port: 5173,
    https: {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath)
    }
  }
});
export {
  vite_config_default as default
};
//# sourceMappingURL=data:application/json;base64,ewogICJ2ZXJzaW9uIjogMywKICAic291cmNlcyI6IFsidml0ZS5jb25maWcudHMiXSwKICAic291cmNlc0NvbnRlbnQiOiBbImNvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9kaXJuYW1lID0gXCJDOlxcXFxVc2Vyc1xcXFxoYW5nb1xcXFxPbmVEcml2ZVxcXFxEZXNrdG9wXFxcXFN3ZWF0U2hlZXRcXFxcc3dlYXRzaGVldC5jbGllbnRcIjtjb25zdCBfX3ZpdGVfaW5qZWN0ZWRfb3JpZ2luYWxfZmlsZW5hbWUgPSBcIkM6XFxcXFVzZXJzXFxcXGhhbmdvXFxcXE9uZURyaXZlXFxcXERlc2t0b3BcXFxcU3dlYXRTaGVldFxcXFxzd2VhdHNoZWV0LmNsaWVudFxcXFx2aXRlLmNvbmZpZy50c1wiO2NvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9pbXBvcnRfbWV0YV91cmwgPSBcImZpbGU6Ly8vQzovVXNlcnMvaGFuZ28vT25lRHJpdmUvRGVza3RvcC9Td2VhdFNoZWV0L3N3ZWF0c2hlZXQuY2xpZW50L3ZpdGUuY29uZmlnLnRzXCI7aW1wb3J0IHsgZGVmaW5lQ29uZmlnIH0gZnJvbSBcInZpdGVcIjtcbmltcG9ydCBwbHVnaW4gZnJvbSBcIkB2aXRlanMvcGx1Z2luLXZ1ZVwiO1xuaW1wb3J0IGZzIGZyb20gXCJmc1wiO1xuaW1wb3J0IHBhdGggZnJvbSBcInBhdGhcIjtcbmltcG9ydCBjaGlsZF9wcm9jZXNzIGZyb20gXCJjaGlsZF9wcm9jZXNzXCI7XG5pbXBvcnQgeyBlbnYgfSBmcm9tIFwicHJvY2Vzc1wiO1xuaW1wb3J0IFZ1ZURldlRvb2xzIGZyb20gXCJ2aXRlLXBsdWdpbi12dWUtZGV2dG9vbHNcIjtcblxuaW1wb3J0IENvbXBvbmVudHMgZnJvbSBcInVucGx1Z2luLXZ1ZS1jb21wb25lbnRzL3ZpdGVcIjtcbmltcG9ydCB7IFZhbnRSZXNvbHZlciB9IGZyb20gXCJAdmFudC9hdXRvLWltcG9ydC1yZXNvbHZlclwiO1xuXG5jb25zdCBiYXNlRm9sZGVyID1cbiAgZW52LkFQUERBVEEgIT09IHVuZGVmaW5lZCAmJiBlbnYuQVBQREFUQSAhPT0gXCJcIlxuICAgID8gYCR7ZW52LkFQUERBVEF9L0FTUC5ORVQvaHR0cHNgXG4gICAgOiBgJHtlbnYuSE9NRX0vLmFzcG5ldC9odHRwc2A7XG5cbmNvbnN0IGNlcnRpZmljYXRlTmFtZSA9IFwic3dlYXRzaGVldC5jbGllbnRcIjtcbmNvbnN0IGNlcnRGaWxlUGF0aCA9IHBhdGguam9pbihiYXNlRm9sZGVyLCBgJHtjZXJ0aWZpY2F0ZU5hbWV9LnBlbWApO1xuY29uc3Qga2V5RmlsZVBhdGggPSBwYXRoLmpvaW4oYmFzZUZvbGRlciwgYCR7Y2VydGlmaWNhdGVOYW1lfS5rZXlgKTtcblxuaWYgKCFmcy5leGlzdHNTeW5jKGNlcnRGaWxlUGF0aCkgfHwgIWZzLmV4aXN0c1N5bmMoa2V5RmlsZVBhdGgpKSB7XG4gIGlmIChcbiAgICAwICE9PVxuICAgIGNoaWxkX3Byb2Nlc3Muc3Bhd25TeW5jKFxuICAgICAgXCJkb3RuZXRcIixcbiAgICAgIFtcImRldi1jZXJ0c1wiLCBcImh0dHBzXCIsIFwiLS1leHBvcnQtcGF0aFwiLCBjZXJ0RmlsZVBhdGgsIFwiLS1mb3JtYXRcIiwgXCJQZW1cIiwgXCItLW5vLXBhc3N3b3JkXCJdLFxuICAgICAgeyBzdGRpbzogXCJpbmhlcml0XCIgfSxcbiAgICApLnN0YXR1c1xuICApIHtcbiAgICB0aHJvdyBuZXcgRXJyb3IoXCJDb3VsZCBub3QgY3JlYXRlIGNlcnRpZmljYXRlLlwiKTtcbiAgfVxufVxuXG5jb25zdCB0YXJnZXQgPSBlbnYuQVNQTkVUQ09SRV9IVFRQU19QT1JUXG4gID8gYGh0dHBzOi8vbG9jYWxob3N0OiR7ZW52LkFTUE5FVENPUkVfSFRUUFNfUE9SVH1gXG4gIDogZW52LkFTUE5FVENPUkVfVVJMU1xuICAgID8gZW52LkFTUE5FVENPUkVfVVJMUy5zcGxpdChcIjtcIilbMF1cbiAgICA6IFwiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAzOFwiO1xuXG4vLyBodHRwczovL3ZpdGVqcy5kZXYvY29uZmlnL1xuZXhwb3J0IGRlZmF1bHQgZGVmaW5lQ29uZmlnKHtcbiAgcGx1Z2luczogW1xuICAgIHBsdWdpbigpLFxuICAgIC8vIFZ1ZURldlRvb2xzKCksXG4gICAgQ29tcG9uZW50cyh7XG4gICAgICByZXNvbHZlcnM6IFtWYW50UmVzb2x2ZXIoKV0sXG4gICAgfSksXG4gIF0sXG4gIHJlc29sdmU6IHtcbiAgICBhbGlhczoge1xuICAgICAgXCJAd29ya291dHNcIjogcGF0aC5yZXNvbHZlKGAke3BhdGgucmVzb2x2ZShfX2Rpcm5hbWUsIFwiLi9zcmMvbW9kdWxlcy93b3Jrb3V0c1wiKX1gKSxcbiAgICAgIFwiQGV4ZXJjaXNlc1wiOiBwYXRoLnJlc29sdmUoYCR7cGF0aC5yZXNvbHZlKF9fZGlybmFtZSwgXCIuL3NyYy9tb2R1bGVzL2V4ZXJjaXNlc1wiKX1gKSxcbiAgICAgIFwiQHNoYXJlZFwiOiBwYXRoLnJlc29sdmUoYCR7cGF0aC5yZXNvbHZlKF9fZGlybmFtZSwgXCIuL3NyYy9tb2R1bGVzL3NoYXJlZFwiKX1gKSxcbiAgICAgIFwiQFwiOiBwYXRoLnJlc29sdmUoX19kaXJuYW1lLCBcIi4vc3JjXCIpLFxuICAgIH0sXG4gIH0sXG4gIHNlcnZlcjoge1xuICAgIHByb3h5OiB7XG4gICAgICBcIl4vYXBpXCI6IHtcbiAgICAgICAgdGFyZ2V0LFxuICAgICAgICBzZWN1cmU6IGZhbHNlLFxuICAgICAgfSxcbiAgICB9LFxuICAgIHBvcnQ6IDUxNzMsXG4gICAgaHR0cHM6IHtcbiAgICAgIGtleTogZnMucmVhZEZpbGVTeW5jKGtleUZpbGVQYXRoKSxcbiAgICAgIGNlcnQ6IGZzLnJlYWRGaWxlU3luYyhjZXJ0RmlsZVBhdGgpLFxuICAgIH0sXG4gIH0sXG59KTtcbiJdLAogICJtYXBwaW5ncyI6ICI7QUFBb1gsU0FBUyxvQkFBb0I7QUFDalosT0FBTyxZQUFZO0FBQ25CLE9BQU8sUUFBUTtBQUNmLE9BQU8sVUFBVTtBQUNqQixPQUFPLG1CQUFtQjtBQUMxQixTQUFTLFdBQVc7QUFHcEIsT0FBTyxnQkFBZ0I7QUFDdkIsU0FBUyxvQkFBb0I7QUFUN0IsSUFBTSxtQ0FBbUM7QUFXekMsSUFBTSxhQUNKLElBQUksWUFBWSxVQUFhLElBQUksWUFBWSxLQUN6QyxHQUFHLElBQUksT0FBTyxtQkFDZCxHQUFHLElBQUksSUFBSTtBQUVqQixJQUFNLGtCQUFrQjtBQUN4QixJQUFNLGVBQWUsS0FBSyxLQUFLLFlBQVksR0FBRyxlQUFlLE1BQU07QUFDbkUsSUFBTSxjQUFjLEtBQUssS0FBSyxZQUFZLEdBQUcsZUFBZSxNQUFNO0FBRWxFLElBQUksQ0FBQyxHQUFHLFdBQVcsWUFBWSxLQUFLLENBQUMsR0FBRyxXQUFXLFdBQVcsR0FBRztBQUMvRCxNQUNFLE1BQ0EsY0FBYztBQUFBLElBQ1o7QUFBQSxJQUNBLENBQUMsYUFBYSxTQUFTLGlCQUFpQixjQUFjLFlBQVksT0FBTyxlQUFlO0FBQUEsSUFDeEYsRUFBRSxPQUFPLFVBQVU7QUFBQSxFQUNyQixFQUFFLFFBQ0Y7QUFDQSxVQUFNLElBQUksTUFBTSwrQkFBK0I7QUFBQSxFQUNqRDtBQUNGO0FBRUEsSUFBTSxTQUFTLElBQUksd0JBQ2YscUJBQXFCLElBQUkscUJBQXFCLEtBQzlDLElBQUksa0JBQ0YsSUFBSSxnQkFBZ0IsTUFBTSxHQUFHLEVBQUUsQ0FBQyxJQUNoQztBQUdOLElBQU8sc0JBQVEsYUFBYTtBQUFBLEVBQzFCLFNBQVM7QUFBQSxJQUNQLE9BQU87QUFBQTtBQUFBLElBRVAsV0FBVztBQUFBLE1BQ1QsV0FBVyxDQUFDLGFBQWEsQ0FBQztBQUFBLElBQzVCLENBQUM7QUFBQSxFQUNIO0FBQUEsRUFDQSxTQUFTO0FBQUEsSUFDUCxPQUFPO0FBQUEsTUFDTCxhQUFhLEtBQUssUUFBUSxHQUFHLEtBQUssUUFBUSxrQ0FBVyx3QkFBd0IsQ0FBQyxFQUFFO0FBQUEsTUFDaEYsY0FBYyxLQUFLLFFBQVEsR0FBRyxLQUFLLFFBQVEsa0NBQVcseUJBQXlCLENBQUMsRUFBRTtBQUFBLE1BQ2xGLFdBQVcsS0FBSyxRQUFRLEdBQUcsS0FBSyxRQUFRLGtDQUFXLHNCQUFzQixDQUFDLEVBQUU7QUFBQSxNQUM1RSxLQUFLLEtBQUssUUFBUSxrQ0FBVyxPQUFPO0FBQUEsSUFDdEM7QUFBQSxFQUNGO0FBQUEsRUFDQSxRQUFRO0FBQUEsSUFDTixPQUFPO0FBQUEsTUFDTCxTQUFTO0FBQUEsUUFDUDtBQUFBLFFBQ0EsUUFBUTtBQUFBLE1BQ1Y7QUFBQSxJQUNGO0FBQUEsSUFDQSxNQUFNO0FBQUEsSUFDTixPQUFPO0FBQUEsTUFDTCxLQUFLLEdBQUcsYUFBYSxXQUFXO0FBQUEsTUFDaEMsTUFBTSxHQUFHLGFBQWEsWUFBWTtBQUFBLElBQ3BDO0FBQUEsRUFDRjtBQUNGLENBQUM7IiwKICAibmFtZXMiOiBbXQp9Cg==

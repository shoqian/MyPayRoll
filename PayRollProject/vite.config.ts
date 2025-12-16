import { defineConfig } from "vite";
import { resolve } from "path";

export default defineConfig({
	root: resolve(__dirname),
	build: {
		manifest: true,
		outDir: resolve(__dirname, "wwwroot/dist"),
		emptyOutDir: true,
		rollupOptions: {
			input: {
				province: resolve(__dirname, "Assets/pages/province/index.ts"),
				// بعداً صفحات دیگر:
				// employee: resolve(__dirname, "Assets/pages/employee/index.ts"),
			},
		},
	},
	server: {
		port: 5173,
		strictPort: true,
	},
});
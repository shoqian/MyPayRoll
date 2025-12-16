import { defineConfig } from 'vite';
import path from 'path';

export default defineConfig({
  build: {
    lib: {
      entry: path.resolve(__dirname, 'src/cityManagement.ts'),
      name: 'CityManagement',
      fileName: () => 'cityManagement.js',
      formats: ['es']
    },
    outDir: path.resolve(__dirname, '../PayRollProject/wwwroot/Pages/CityManagement/dist'),
    emptyOutDir: true
  }
});

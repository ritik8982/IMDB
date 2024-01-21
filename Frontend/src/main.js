import { registerPlugins } from '@/plugins'

import App from './App.vue'

import { createApp } from 'vue'
import router from '../router.js'
import store from '../store/index.js'

const app = createApp(App)

app.use(router);
app.use(store);

registerPlugins(app)

app.mount('#app')

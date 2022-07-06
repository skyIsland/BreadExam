import Vue from 'vue'
import App from './App'
import Common from './Common'
import testPage from './pages/component/testpage.vue'
import MescrollBody from "./components/mescroll-uni/mescroll-body.vue"
import MescrollUni from "./components/mescroll-uni/mescroll-uni.vue"
import cuCustom from './colorui/components/cu-custom.vue'
Vue.component('cu-custom',cuCustom)
Vue.component('test-page',testPage)
Vue.component('mescroll-body', MescrollBody)
Vue.component('mescroll-uni', MescrollUni)	
Vue.prototype.Common = Common;

Vue.config.productionTip = false

App.mpType = 'app'

const app = new Vue({
    ...App
})
app.$mount()

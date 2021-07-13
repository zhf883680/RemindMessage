"use strict";

import Vue from 'vue';
import axios from "axios";
import {
    Message
} from 'element-ui';
// Full config:  https://github.com/axios/axios#request-config
// axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '';
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
axios.defaults.headers.post['Content-Type'] = 'application/json';

//axios.defaults.baseURL = env === 'development' ? 'http://localhost:8050' : window.location.protocol + '//' + window.location.host; // 配置axios请求的地址
//调试模式下 访问地址
if (process.env.NODE_ENV === "development") {
    axios.defaults.baseURL = 'http://localhost:7000' // 配置axios请求的地址
}
axios.defaults.crossDomain = true;
axios.defaults.withCredentials = true; //设置cross跨域 并设置访问权限 允许跨域携带cookie信息
//axios.defaults.headers.common['Authorization'] = "Bearer " + sessionStorage.AuthorizationSdc;

const _axios = axios.create();

_axios.interceptors.request.use(
    function (config) {
        config.headers.Authorization = "Bearer " + sessionStorage.Authorization;
        // Do something before request is sent
        return config;
    },
    function (error) {
        // Do something with request error
        return Promise.reject(error);
    }
);

// Add a response interceptor
_axios.interceptors.response.use(
    function (response) {
        if (response.data.errcode !== 0) {
            Message.error(response.data.errmsg);
        } 
        return response.data;
    },
    function (err) {
        if (err.message === "Network Error") {
            Message.error("与服务器连接失败,请检查网络与服务器状态,若服务器正在初始化,请耐心等候");
        } else if (err.response !== undefined) {
            if (err.response.status === 429) {
                Message.error("请求次数过多,请过5分钟后重试");
            } else if (err.response.status === 401) {
                Message.error("账户认证失败,3s后返回登陆页面");
                sessionStorage.removeItem("Authorization");
                setTimeout(function () {
                    location.href = "/"
                }, 3000);
            } else if (err.response.status === 403) {
                Message.error("您无权限访问此页面");
                setTimeout(function () {
                    location.href = "/"
                }, 3000);
            } else {
                Message.error("操作失败,请检查数据格式");
            }
        } else {
            Message.error("操作失败,请检查数据格式");
        }
        // Do something with response error
        return Promise.reject(err);
    }
);
// eslint-disable-next-line no-unused-vars
Plugin.install = function (Vue, options) {
    Vue.axios = _axios;
    window.axios = _axios;
    Object.defineProperties(Vue.prototype, {
        axios: {
            get() {
                return _axios;
            }
        },
        $axios: {
            get() {
                return _axios;
            }
        },
    });
};

Vue.use(Plugin)

export default Plugin;
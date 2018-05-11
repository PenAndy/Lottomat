/*公共Jscript*/
var Common = {
    //接口主机地址
    API_BASE_HOST: "http://m.api.55128.cn",
    //根据key移除服务器缓存
    API_URL_REMOVE_CACHE_BY_KEY_URL: "/api/v1/Common/RemoveCacheByKey",
    //组装请求Url
    GET_REMOVE_CACHE_API_URL: function (code) {
        code = code == null ? "All" : code;
        return this.API_BASE_HOST + this.API_URL_REMOVE_CACHE_BY_KEY_URL + "?type=" + code;
    },
};
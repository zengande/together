const env = process.env.NODE_ENV

export default {
    // 当前应用地址
    AppBaseAddress: env === "production" ? "https://together2.fun" : "http://localhost:3000",
    // API 地址
    ApiBaseAddress: env === "production" ? "https://api.together2.fun" : "http://localhost:3000/api"
}
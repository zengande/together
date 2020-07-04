const env = process.env.NODE_ENV
export default {
    // 当前应用地址
    AppBaseAddress: env === "development" ? "http://localhost:3000" : "https://together2.fun",
    // API 地址
    ApiBaseAddress: env === "development" ? "http://localhost:4000" : "https://api.together2.fun"
}
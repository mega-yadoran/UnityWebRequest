using System;

namespace Api
{
    /// <summary>
    /// POSTリクエストのサンプルAPIをコールします
    /// 存在しないURLにリクエストを投げるため、失敗します
    /// </summary>
    public class PostSampleApi : BaseApi
    {
        /// リクエストパラメータ
        [Serializable]
        public struct Request
        {
            public string param1;
        }
        public Request request;

        /// レスポンスデータ
        [Serializable]
        public struct Response
        {
            public int res1;
        }
        public Response response;

        // コンストラクタ
        public PostSampleApi(string param1)
        {
            BaseUrl = "http://post-sample.example.com";
            EndPoint = "/test";
            Headers.Add("content-type", "application/json");
            request.param1 = param1;
        }
    }
}
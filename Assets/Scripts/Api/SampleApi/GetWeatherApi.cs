using System;

namespace Api
{
    /// <summary>
    /// 天気予報 API（livedoor 天気互換）(https://weather.tsukumijima.net/)から現在の天気情報を取得します
    /// </summary>
    public class GetWeatherApi : BaseApi
    {
        /// <summary>
        /// リクエストパラメータ
        /// </summary>
        [Serializable]
        public struct Request
        {
            public int city;
        }
        public Request request;

        /// <summary>
        /// レスポンスパラメータ
        /// </summary>
        [Serializable]
        public struct Response
        {
            public string publicTimeFormatted;
            public string title;
            public Description description; // 階層構造になっている場合は子も構造体で定義する
        }
        public Response response;

        [Serializable]
        public struct Description
        {
            public string publicTimeFormatted;
            public string headlineText;
            public string text;
        }

        public GetWeatherApi(int city)
        {
            BaseUrl = "https://weather.tsukumijima.net/api";
            EndPoint = "/forecast";
            request.city = city;
        }
    }
}
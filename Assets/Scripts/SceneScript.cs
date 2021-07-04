using UnityEngine;

public class SceneScript : MonoBehaviour
{
    void Start()
    {
        // GETリクエストのサンプル
        // 天気予報 API（livedoor 天気互換）(https://weather.tsukumijima.net/) をコールします
        // 特に問題がなければリクエストに成功するはずです
        int cityId = 400040; // 福岡県久留米の地域ID
        Api.GetWeatherApi getApi = new Api.GetWeatherApi(cityId);
        getApi.Get<Api.GetWeatherApi.Request>(ref getApi.request, result =>
        {
            if (result.isSuccess)
            {
                // リクエストに成功した場合
                getApi.response = getApi.Response<Api.GetWeatherApi.Response>();
                Debug.Log("天気予報APIのリクエストに成功しました");
                Debug.Log(getApi.response.publicTimeFormatted);
                Debug.Log(getApi.response.description.text);
            }
            else
            {
                // リクエストに失敗した場合
                Debug.Log("天気予報APIリクエストに失敗しました");
                Debug.Log(result.error);
            }
        });

        // POSTリクエストのサンプル
        // PostSampleApi をコールします
        // 存在しないURLなので失敗します
        string param1 = "test";
        Api.PostSampleApi postApi = new Api.PostSampleApi(param1);
        postApi.Post<Api.PostSampleApi.Request>(ref postApi.request, result =>
        {
            if (result.isSuccess)
            {
                getApi.response = getApi.Response<Api.GetWeatherApi.Response>();

                Debug.Log("PostSampleApiのリクエストに成功しました");
            }
            else
            {
                Debug.Log("PostSampleApiリクエストに失敗しました");
                Debug.Log(result.error);
            }
        });
    }
}

using UnityEngine;

public class SceneScript : MonoBehaviour
{
    void Start()
    {
        // GETリクエストのサンプル
        // 天気予報 API（livedoor 天気互換）(https://weather.tsukumijima.net/) をコールします
        // 特に問題がなければリクエストに成功するはずです
        int cityId = 400040; // 福岡県久留米の地域ID
        Api.GetWeatherApi api1 = new Api.GetWeatherApi(cityId);
        api1.Get<Api.GetWeatherApi.Request>(ref api1.request, result =>
        {
            if (result.isSuccess)
            {
                api1.response = api1.Response<Api.GetWeatherApi.Response>();

                Debug.Log("天気予報APIのリクエストに成功しました");
                Debug.Log(api1.response.publicTimeFormatted);
                Debug.Log(api1.response.description.text);
            }
            else
            {
                Debug.Log("天気予報APIリクエストに失敗しました");
                Debug.Log(result.error);
            }
        });


        // POSTリクエストのサンプル
        // PostSampleApi をコールします
        // 存在しないURLなので失敗します
        string param1 = "test";
        Api.PostSampleApi api2 = new Api.PostSampleApi(param1);
        api2.Post<Api.PostSampleApi.Request>(ref api2.request, result =>
        {
            if (result.isSuccess)
            {
                api1.response = api1.Response<Api.GetWeatherApi.Response>();

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// HTTP 通信基底クラス
/// </summary>
public class BaseApi
{
    /// <summary>
    /// HTTP 通信の結果
    /// </summary>
    public struct Result
    {
        public bool isSuccess; // true で成功
        public string error; // 失敗時のエラー内容

        /// <summary>
        /// 成功
        /// </summary>
        public void Success()
        {
            isSuccess = true;
            error = "";
        }

        /// <summary>
        /// 失敗
        /// </summary>
        /// <param name="error"></param>
        public void Failed(string error)
        {
            isSuccess = false;
            this.error = error;
        }
    }

    // ベースURL
    protected string BaseUrl;
    // エンドポイント
    protected string EndPoint;
    // ヘッダー
    protected Dictionary<string, string> Headers = new Dictionary<string, string>();
    // レスポンス（JSON）
    private string resJson;

    /// <summary>
    /// リクエストデータ（オブジェクト）を クエリストリング に変換して HTTP（GET）通信を行う
    /// </summary>
    /// <typeparam name="T">リクエストデータ型</typeparam>
    /// <param name="data">リクエストのオブジェクト</param>
    /// <param name="cb">コールバック</param>    
    public void Get<T>(ref T data, Action<Result> cb)
    {
        // リクエストオブジェクトを クエリストリング に変換
        string queryString = UrlHelpers.ToQueryString(data);

        // HTTP（GET）通信
        string url = BaseUrl + EndPoint + queryString;
        CoroutineHandler.StartStaticCoroutine(onGetSend(url, cb));
    }

    /// <summary>
    /// HTTP（GET）通信の実行
    /// </summary>
    /// <param name="url">接続する URL</param>
    /// <param name="cb">コールバック</param>
    /// <returns>コルーチン</returns>
    private IEnumerator onGetSend(string url, Action<Result> cb)
    {
        // HTTP（GET）の情報を設定
        UnityWebRequest req = UnityWebRequest.Get(url);

        // リクエストヘッダーの設定
        foreach (var h in Headers)
        {
            req.SetRequestHeader(h.Key, h.Value);
        }

        // API 通信（完了待ち）
        yield return req.SendWebRequest();

        // 通信結果
        Result result = new Result();
        if (req.isNetworkError || req.isHttpError) // 失敗
        {
            // Debug.Log("Network error:" + req.error);
            result.Failed(req.error);
        }
        else // 成功
        {
            // Debug.Log("Success:" + req.downloadHandler.text);
            resJson = req.downloadHandler.text;
            result.Success();
        }
        cb(result);
    }

    /// <summary>
    /// リクエストデータ（オブジェクト）を WWWForm に変換して HTTP（POST）通信を行う
    /// </summary>
    /// <typeparam name="T">リクエスト型</typeparam>
    /// <param name="data">リクエストデータのオブジェクト</param>
    /// <param name="cb">コールバック</param>    
    public void Post<T>(ref T data, Action<Result> cb)
    {
        // リクエストオブジェクトを WWWForm に変換
        WWWForm form = UrlHelpers.ToWWWForm(data);

        // HTTP（POST）通信
        string url = BaseUrl + EndPoint;
        CoroutineHandler.StartStaticCoroutine(onPostSend(url, form, cb));
    }

    /// <summary>
    /// HTTP（POST）通信の実行
    /// </summary>
    /// <param name="url">接続する URL</param>
    /// <param name="form">POST するデータ</param>
    /// <param name="cb">コールバック</param>
    /// <returns>コルーチン</returns>
    private IEnumerator onPostSend(string url, WWWForm form, Action<Result> cb)
    {
        // HTTP（POST）の情報を設定
        UnityWebRequest req = UnityWebRequest.Post(url, form);

        // リクエストヘッダーの設定
        foreach (var h in Headers)
        {
            req.SetRequestHeader(h.Key, h.Value);
        }

        // API 通信（完了待ち）
        yield return req.SendWebRequest();

        // 通信結果
        Result result = new Result();
        if (req.isNetworkError || req.isHttpError) // 失敗
        {
            // Debug.Log("Network error:" + req.error);
            result.Failed(req.error);
        }
        else // 成功
        {
            // Debug.Log("Success:" + req.downloadHandler.text);
            resJson = req.downloadHandler.text;
            result.Success();
        }
        cb(result);
    }

    /// <summary>
    /// レスポンス（JSON）からオブジェクトを生成して返す
    /// </summary>
    /// <typeparam name="T">レスポンス型</typeparam>
    /// <returns>レスポンスのオブジェクト</returns>
    public T Response<T>()
    {
        return JsonUtility.FromJson<T>(resJson);
    }
}
using LitJson;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace B13Port.Common
{
    public class HttpManager : MonoSingletion<HttpManager>
    {

        public void PostUrl(string url, string postData, Action<JsonData> callBack, string authorization)
        {
            StartCoroutine(Post(url, postData, callBack, authorization));
        }

        public void PostUrl(string url, WWWForm data, Action<BasetData> callBack)
        {
            StartCoroutine(Post(url, data, callBack));
        }

        private IEnumerator Post(string url, string postData, Action<JsonData> callBack, string authorization)
        {
            using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
            {
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postData);
                webRequest.uploadHandler = new UploadHandlerRaw(postBytes);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                if (!string.IsNullOrEmpty(authorization))
                {
                    webRequest.SetRequestHeader("Authorization", authorization);
                }
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.SetRequestHeader("Request-ID", CommonTool.GetTimeStamp());
                yield return webRequest.SendWebRequest();
                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    XDebug.Log("请求地址：" + url, LogHelper.NetInfo);
                    XDebug.Log(webRequest.error, LogHelper.NetInfo);
                }
                else
                {
                    try
                    {
                        JsonData Jsonobj = JsonMapper.ToObject(webRequest.downloadHandler.text);

                        XDebug.Log(webRequest.downloadHandler.text, LogHelper.NetInfo);
                        //callBack?.Invoke(Jsonobj);
                    }
                    catch (Exception ex)
                    {
                        XDebug.LogError("请求地址：" + url, LogHelper.NetInfo);
                        XDebug.LogError($"Exception:{ex}", LogHelper.NetInfo);
                        XDebug.LogError(webRequest.downloadHandler.text, LogHelper.NetInfo);
                    }
                }
            }
        }


        private IEnumerator Post(string url, WWWForm data, Action<BasetData> callBack)
        {
            using (UnityWebRequest request = UnityWebRequest.Post(url, data))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    XDebug.Log("请求地址：" + url, LogHelper.NetInfo);
                    XDebug.Log(request.error, LogHelper.NetInfo);
                }
                else
                {
                    try
                    {
                        BasetData Jsonobj = JsonMapper.ToObject<BasetData>(request.downloadHandler.text);
                        XDebug.Log(request.downloadHandler.text, LogHelper.NetInfo);
                        callBack?.Invoke(Jsonobj);
                    }
                    catch (Exception ex)
                    {
                        XDebug.Log("请求地址：" + url, LogHelper.NetInfo);
                        XDebug.LogError($"Exception:{ex}", LogHelper.NetInfo);
                        XDebug.LogError(request.downloadHandler.text, LogHelper.NetInfo);
                    }
                }
            }
        }
    }
}

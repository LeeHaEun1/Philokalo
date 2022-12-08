using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum RequestType
{
    POST,
    GET,
    PUT,
    DELETE
}

public class HttpRequester : MonoBehaviour
{
    // 요청 타입(GET, POST, PUT, DELETE)
    public RequestType requestType;

    // http://3.38.39.121:8080/~~
    public string url;

    // body 데이터
    public string jsonText;
    public byte[] body;

    // 통신 성공 시(200) (201도 여기 해당되나?)
    public Action<DownloadHandler> onComplete;

    // 통신 실패 시(400)
    public Action onError;
}

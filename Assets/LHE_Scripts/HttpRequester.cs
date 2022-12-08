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
    // ��û Ÿ��(GET, POST, PUT, DELETE)
    public RequestType requestType;

    // http://3.38.39.121:8080/~~
    public string url;

    // body ������
    public string jsonText;
    public byte[] body;

    // ��� ���� ��(200) (201�� ���� �ش�ǳ�?)
    public Action<DownloadHandler> onComplete;

    // ��� ���� ��(400)
    public Action onError;
}

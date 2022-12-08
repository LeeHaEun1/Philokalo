using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpManager : MonoBehaviour
{
    public static HttpManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ������ ��û
    public void SendRequest(HttpRequester requester)
    {
        StartCoroutine(Send(requester));
    }

    // Coroutine
    IEnumerator Send(HttpRequester requester)
    {
        UnityWebRequest webRequest = null;
        
        // ��� ���� �Լ� -> �̰��� signpuManager���� ��� �־����� ����!!
        //webRequest.SetRequestHeader
        switch (requester.requestType)
        {
            case RequestType.POST:
                webRequest = UnityWebRequest.Post(requester.url, requester.jsonText);
                break;
            case RequestType.GET:
                webRequest = UnityWebRequest.Get(requester.url);
                break;
            case RequestType.PUT:
                webRequest = UnityWebRequest.Put(requester.url, requester.body);
                break;
            case RequestType.DELETE:
                webRequest = UnityWebRequest.Delete(requester.url);
                break;
        }

        // Header
        webRequest.SetRequestHeader("Accept", "*/*");
        webRequest.SetRequestHeader("Content-Type", "application/json");
        // ******** ��ū ��� ��� ********
        // �ڳ���� ����/���� �ÿ��� �θ���ū ������,, 
        // �ΰ��� ������ �۽� �� �α׾ƿ� �ÿ��� �ڳ���ū ������,,?
        // *** �ڳ���� ���� �������� �θ���ū, ���� ���Ŀ��� �ڳ� ��ū ������ ���� ���̽� ������ �ʿ������?
        webRequest.SetRequestHeader("Authorization", "Bearer " + TokenManager.Instance.token);

        webRequest.uploadHandler = new UploadHandlerRaw(requester.body);

        // ������ ��û ������ ���� ���
        yield return webRequest.SendWebRequest();

        // ����
        if(webRequest.result == UnityWebRequest.Result.Success)
        {
            // ���� ���� ���� ���
            print(webRequest.downloadHandler.text);

            // onComplete�� �ش��ϴ� �Լ� ����
            requester.onComplete(webRequest.downloadHandler);
        }
        // ����
        else
        {
            // ���� ���� ���� ���
            print(webRequest.downloadHandler.text);                                   

            // Error message
            print("��� ���� " + webRequest.result + "\n" + webRequest.error);

            requester.onError();
        }
    }
}

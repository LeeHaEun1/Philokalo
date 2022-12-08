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

    // 서버에 요청
    public void SendRequest(HttpRequester requester)
    {
        StartCoroutine(Send(requester));
    }

    // Coroutine
    IEnumerator Send(HttpRequester requester)
    {
        UnityWebRequest webRequest = null;
        
        // 헤더 설정 함수 -> 이것을 signpuManager에서 어떻게 넣어줄지 생각!!
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
        // ******** 토큰 담는 헤더 ********
        // 자녀계정 생성/접속 시에는 부모토큰 보내고,, 
        // 인게임 데이터 송신 및 로그아웃 시에는 자녀토큰 보내고,,?
        // *** 자녀계정 접속 이전에는 부모토큰, 접속 이후에는 자녀 토큰 보내니 굳이 케이스 나눠줄 필요없을듯?
        webRequest.SetRequestHeader("Authorization", "Bearer " + TokenManager.Instance.token);

        webRequest.uploadHandler = new UploadHandlerRaw(requester.body);

        // 서버에 요청 보내고 응답 대기
        yield return webRequest.SendWebRequest();

        // 성공
        if(webRequest.result == UnityWebRequest.Result.Success)
        {
            // 서버 응답 내용 출력
            print(webRequest.downloadHandler.text);

            // onComplete에 해당하는 함수 실행
            requester.onComplete(webRequest.downloadHandler);
        }
        // 실패
        else
        {
            // 서버 응답 내용 출력
            print(webRequest.downloadHandler.text);                                   

            // Error message
            print("통신 실패 " + webRequest.result + "\n" + webRequest.error);

            requester.onError();
        }
    }
}

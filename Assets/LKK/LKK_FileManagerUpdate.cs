using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;


public class LKK_FileManagerUpdate : MonoBehaviour
{
    #region 이전시도
    // 1. 동영상 파일 1차 시도
    // public RawImage rawImage;
    // 
    // public void OpenFileBrowser()
    // {
    //     var bp = new BrowserProperties();
    //     // bp.filter = "mp4 files (*.mp4) | All Files *.mp4";
    //     bp.filter = "mp4 files (*.mp4)|*.mp4|All Files (*.*)|*.*";
    //     bp.filterIndex = 0;
    // 
    //     new FileBrowser().OpenFileBrowser(bp, path =>
    //     {
    //         StartCoroutine(LoadImage(path));
    //     });
    // }
    // IEnumerable LoadImage(string path)
    // {
    //     using (UnityWebRequest urw = UnityWebRequestTexture.GetTexture(path)) 
    //     {
    //         yield return urw.SendWebRequest();
    // 
    //         if(urw.isNetworkError || urw.isHttpError)
    //         {
    //             Debug.Log(urw.error);
    //         }
    //         else
    //         {
    //             var uwrTexture = DownloadHandlerTexture.GetContent(urw);
    //             rawImage.texture = uwrTexture;
    //         }
    //     }
    // }

    // 2차 시도
    // string[] paths;
    // public RawImage rawImage;
    // 
    // public void OpenExplorer()
    // {
    //     //path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png"); // Build Error 발생
    //     paths = StandaloneFileBrowser.OpenFilePanel("Overwrite with png", "", "png", false); // Build는 진행되는거 같은데 시간 너무 오래 걸려 테스트 못해봄..
    //     GetImage();
    // }
    // 
    // public void GetImage()
    // {
    //     if (paths != null)
    //     {
    //         StartCoroutine(UploadImage());
    //     }
    // }
    // 
    // public IEnumerator UploadImage()
    // {
    //     //Texture2D texture = Selection.activeObject as Texture2D;
    //     //WWW www = new WWW("file:///" + path);
    //     //rawImage.texture = www.texture;
    //     //var fileContent = File.ReadAllBytes(path);
    //     //texture.LoadImage(fileContent);
    // 
    // 
    //     //UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + path);
    //     //UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path);
    //     //yield return uwr.SendWebRequest();
    // 
    //     //rawImage.texture = DownloadHandlerTexture.GetContent(uwr);
    // 
    // 
    // 
    //     using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + paths[0]))
    //     {
    //         yield return uwr.SendWebRequest();
    // 
    //         if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
    //         {
    //             Debug.Log(uwr.error);
    //         }
    //         else
    //         {
    //             var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
    //             rawImage.texture = uwrTexture;
    //             rawImage.color = new Color(1, 1, 1, 1);
    //         }
    //     }
    // }
    #endregion

    string path;
    public RawImage image;
    public GameObject panelobject;

    private void Start()
    {
        panelobject.SetActive(false);
    }
    private void Update()
    {
        QuitImage();
    }

    public void OpenExplorer()
    {
        // 만약 오픈익스플로러 함수가 열리는 경우, 
        // 에디터 유틸리티의 오픈파일패널을 열고
        // path = EditorUtility.OpenFilePanel("Overwrite with mp4", "D:\\DownLoadVideo", "mp4");
        // 영상재생을 위한 Panel_Load_Video_Raw_Image 을 활성화 시킨 후 
        panelobject.SetActive(true);
        // GetImage() 함수를 실행시킨다.
        GetImage();
    }

    void GetImage()
    {
        if(path !=null) 
        {
            UpdateImage();
        }
    }
    void UpdateImage()
    {
        WWW www = new("file:///" + path);
        image.texture = www.texture;
    }

    void QuitImage()
    {
        if(panelobject != null) 
        {
            // panelobject 범위 바깥을 클릭하면
            // panelobject 의 상태를 SetActive(false)로 만든다.
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    //클릭 처리
                    panelobject.SetActive(false);
                }

            }
        }
    }
}

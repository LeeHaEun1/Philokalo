using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;


public class LKK_FileManagerUpdate : MonoBehaviour
{
    #region �����õ�
    // 1. ������ ���� 1�� �õ�
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

    // 2�� �õ�
    // string[] paths;
    // public RawImage rawImage;
    // 
    // public void OpenExplorer()
    // {
    //     //path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png"); // Build Error �߻�
    //     paths = StandaloneFileBrowser.OpenFilePanel("Overwrite with png", "", "png", false); // Build�� ����Ǵ°� ������ �ð� �ʹ� ���� �ɷ� �׽�Ʈ ���غ�..
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
        // ���� �����ͽ��÷η� �Լ��� ������ ���, 
        // ������ ��ƿ��Ƽ�� ���������г��� ����
        // path = EditorUtility.OpenFilePanel("Overwrite with mp4", "D:\\DownLoadVideo", "mp4");
        // ��������� ���� Panel_Load_Video_Raw_Image �� Ȱ��ȭ ��Ų �� 
        panelobject.SetActive(true);
        // GetImage() �Լ��� �����Ų��.
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
            // panelobject ���� �ٱ��� Ŭ���ϸ�
            // panelobject �� ���¸� SetActive(false)�� �����.
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    //Ŭ�� ó��
                    panelobject.SetActive(false);
                }

            }
        }
    }
}

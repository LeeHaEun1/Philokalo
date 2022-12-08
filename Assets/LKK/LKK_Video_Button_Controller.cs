using RockVR.Video;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKK_Video_Button_Controller : MonoBehaviour
{
    public bool isPlayVideo;

    private void Awake()
    {

        Application.runInBackground = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        isPlayVideo = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print(VideoCaptureCtrl.instance.status);
    }

    public void Record_Video_Capture()
    {
        //if (isPlayVideo == false)
        //{
            VideoCaptureCtrl.instance.StartCapture();
        //    isPlayVideo = true;
        //}

        #region 이전내용 주석
        //print(isPlayVideo.ToString());
        //if(isPlayVideo == false)
        //{
        //    isPlayVideo = true;
        //    VideoCaptureCtrl.instance.StartCapture();
        //}
        #endregion

    }
    public void Stop_Record_Video_Capture()
    {
        //if (isPlayVideo == true)
        //{
            VideoCaptureCtrl.instance.StopCapture();
        //    isPlayVideo = false;
        //}
    }
}

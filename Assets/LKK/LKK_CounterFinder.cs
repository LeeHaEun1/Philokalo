using OpenCvSharp;
using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 익스텐션기능 못쓰나요?
// using OpenCvSharp.Extensions;
using System.Threading;
using UnityEngine.Windows.WebCam;
using System;

using RockVR;
using RockVR.Video;

public class LKK_CounterFinder : WebCamera
{
    [SerializeField] private FlipMode ImageFlip;
    [SerializeField] private float Threshold = 96.4f;
    [SerializeField] private bool ShowProcessingImage = true;
    [SerializeField] private float CurveAccuracy = 10f;
    [SerializeField] private float MinArea = 5000f;

    private Mat image;
    private Mat processImage = new Mat();
    private Point[][] contours;
    private HierarchyIndex[] hierarchy;
    // public VideoCaptureCtrl vcCtrl;

    private bool isPlayVideo = false;
    private void Start()
    {
        isPlayVideo = false;
    }

    // 1. 참고: RawImage에 frame 단위로 계속 사진을 가져오기 힘드니, 이 함수를 사용하여 한번 가져오고 끝내려고 작성함.
    // 2. 기능: RawImage에 image를 가져와서 그것을 openCV 내부에서 가동하도록 작성함.
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        image = OpenCvSharp.Unity.TextureToMat(input);

        Cv2.Flip(image, image, ImageFlip);
        // RawImage의 방향을 설정한 축에 따라 변경할 수 있다.
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
        // 색상의 반전을 준다.
        Cv2.Threshold(processImage, processImage, Threshold, 255, ThresholdTypes.BinaryInv);
        // 사물의 아웃라인(한계점)을 찾아주는 함수

        Cv2.FindContours(processImage, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);
        // 사물의 이미지, 그리고 그것을 파악하기 위한 Pixel 묶음에 대한 contours(고차배열), 구현할 위치, 사용알고리즘, 그리고 배열좌표값을 단순화 시켜줄 알고리즘을 적용한다.)
        foreach (Point[] contour in contours)
        {
            Point[] points = Cv2.ApproxPolyDP(contour, CurveAccuracy, true);
            // 어느정도의 density를 가질지 확인하기 위하여 다음과 같이 진행함.(객체의 넓이 정도로 번역됨.)
            var area = Cv2.ContourArea(contour);

            if (area > MinArea)
            {
                drawContour(processImage, new Scalar(127, 127, 127), 2, points);
            }
        }

        // Do cool Processing stuff
        if (output == null)
        {
            // Show processing image
            // output = OpenCvSharp.Unity.MatToTexture(image);
            // ? 내가 원하는 무엇을 표현하던지간에, 그 표현(ShowProcessingImage)에 대한 bool 연산자의 값이 true 라면 processImage 를 반환하고 false 라면 image를 반환한다.
            output = OpenCvSharp.Unity.MatToTexture(ShowProcessingImage ? processImage : image);
        }
        else
        {
            // OpenCvSharp.Unity.MatToTexture(image, output);
            OpenCvSharp.Unity.MatToTexture(ShowProcessingImage ? processImage : image, output);
        }
        return true;

    }

    // 윤곽선에 해당하는 Pixel 묶음에 대한 contours(고차배열)을 생성해 준다.
    private void drawContour(Mat Image, Scalar Color, int Thickness, Point[] Points)
    {
        for (int i = 1; i < Points.Length; i++)
        {
            Cv2.Line(Image, Points[i - 1], Points[i], Color, Thickness);
            // 라인 그려주기
        }
        Cv2.Line(Image, Points[Points.Length - 1], Points[0], Color, Thickness);
    }



    // 해당함수 발동 시, 영상을 생성한다.ㄹ
    public void Record_Video_Capture()
    {
        VideoCaptureCtrl.instance.StartCapture();

        //print(isPlayVideo.ToString());
        //if(isPlayVideo == false)
        //{
        //    isPlayVideo = true;
        //    VideoCaptureCtrl.instance.StartCapture();
        //}

    }
    public void Stop_Record_Video_Capture()
    {
        if (isPlayVideo == true)
        {
            isPlayVideo = false;
            VideoCaptureCtrl.instance.StopCapture();
        }
    }

}
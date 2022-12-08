using OpenCvSharp;
using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ͽ��ټǱ�� ��������?
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

    // 1. ����: RawImage�� frame ������ ��� ������ �������� �����, �� �Լ��� ����Ͽ� �ѹ� �������� �������� �ۼ���.
    // 2. ���: RawImage�� image�� �����ͼ� �װ��� openCV ���ο��� �����ϵ��� �ۼ���.
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        image = OpenCvSharp.Unity.TextureToMat(input);

        Cv2.Flip(image, image, ImageFlip);
        // RawImage�� ������ ������ �࿡ ���� ������ �� �ִ�.
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
        // ������ ������ �ش�.
        Cv2.Threshold(processImage, processImage, Threshold, 255, ThresholdTypes.BinaryInv);
        // �繰�� �ƿ�����(�Ѱ���)�� ã���ִ� �Լ�

        Cv2.FindContours(processImage, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);
        // �繰�� �̹���, �׸��� �װ��� �ľ��ϱ� ���� Pixel ������ ���� contours(�����迭), ������ ��ġ, ���˰���, �׸��� �迭��ǥ���� �ܼ�ȭ ������ �˰����� �����Ѵ�.)
        foreach (Point[] contour in contours)
        {
            Point[] points = Cv2.ApproxPolyDP(contour, CurveAccuracy, true);
            // ��������� density�� ������ Ȯ���ϱ� ���Ͽ� ������ ���� ������.(��ü�� ���� ������ ������.)
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
            // ? ���� ���ϴ� ������ ǥ���ϴ�������, �� ǥ��(ShowProcessingImage)�� ���� bool �������� ���� true ��� processImage �� ��ȯ�ϰ� false ��� image�� ��ȯ�Ѵ�.
            output = OpenCvSharp.Unity.MatToTexture(ShowProcessingImage ? processImage : image);
        }
        else
        {
            // OpenCvSharp.Unity.MatToTexture(image, output);
            OpenCvSharp.Unity.MatToTexture(ShowProcessingImage ? processImage : image, output);
        }
        return true;

    }

    // �������� �ش��ϴ� Pixel ������ ���� contours(�����迭)�� ������ �ش�.
    private void drawContour(Mat Image, Scalar Color, int Thickness, Point[] Points)
    {
        for (int i = 1; i < Points.Length; i++)
        {
            Cv2.Line(Image, Points[i - 1], Points[i], Color, Thickness);
            // ���� �׷��ֱ�
        }
        Cv2.Line(Image, Points[Points.Length - 1], Points[0], Color, Thickness);
    }



    // �ش��Լ� �ߵ� ��, ������ �����Ѵ�.��
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
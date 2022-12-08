using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Unity.VisualScripting;

public class LKK_Timer : MonoBehaviour
{
    // public Thread thread;
    // private Queue<string> queue = new Queue<string>();
    // public Text textBox;


    // Start is called before the first frame update
    void Start()
    {
        // thread = new Thread(Run);
        // thread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // if (queue.Count > 0)
        // {
        //     textBox.text = queue.Dequeue(); // UI처리
        //     Debug.Log(textBox.text);
        // }
    }

    // private void Run()
    // {
    //     while (true)
    //     {
    //         string s = "text";
    //         queue.Enqueue(s);
    //         // textBox.text = s; // 쓰레드는 처리될 데이터를 규에 담기
    //         Thread.Sleep(1000);
    //     }
    // }

    // private void OnApplicationQuit()
    // {
    //     if ((thread != null))
    //     {
    //         thread.Abort();
    //     }
    // }
}

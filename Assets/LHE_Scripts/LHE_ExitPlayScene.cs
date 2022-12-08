using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHE_ExitPlayScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 에디터와 빌드 차이
    public void OnClickQuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

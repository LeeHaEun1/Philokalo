using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHE_RollplayingScriptManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true);
        script1.SetActive(false);
        canvasRollplaying.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Scenario Button
    public Canvas canvasRollplaying;
    public void ScriptCanvasActivate()
    {
        if(canvasRollplaying.enabled == false)
        {
            canvasRollplaying.enabled = true;
        }
        else if(canvasRollplaying.enabled == true)
        {
            canvasRollplaying.enabled = false;
        }
    }

    public GameObject menu;

    // [     Script1     ]
    // Script1 Select Button
    [Header("[ Script 1 ]")]
    public GameObject script1;
    public void Script1Open()
    {
        menu.SetActive(false);
        script1.SetActive(true);
    }

    // Script1 Back to Menu Button
    public void Script1Close()
    {
        menu.SetActive(true);
        script1.SetActive(false);
    }

    // Script1 Page Up Button
    public List<GameObject> script1Pages = new List<GameObject>();
    public void Script1PageUp()
    {
        int curr = 0;

        for(int i = 0; i < script1Pages.Count; i++)
        {
            if(script1Pages[i].activeSelf == true)
            {
                curr = i;
            }
        }

        if(curr != (script1Pages.Count - 1))
        {
            script1Pages[curr].SetActive(false);
            script1Pages[curr + 1].SetActive(true);
        }
        else
        {
            return;
        }
    }

    // Script1 Page Down Button
    public void Script1PageDown()
    {
        int curr = 0;

        for (int i = 0; i < script1Pages.Count; i++)
        {
            if (script1Pages[i].activeSelf == true)
            {
                curr = i;
            }
        }

        if (curr != 0)
        {
            script1Pages[curr].SetActive(false);
            script1Pages[curr - 1].SetActive(true);
        }
        else
        {
            return;
        }
    }

    // [     Script2     ]
    // Script1 Select Button
    [Header("[ Script 2 ]")]
    public GameObject script2;
    public void Script2Open()
    {
        menu.SetActive(false);
        script2.SetActive(true);
    }

    // Script1 Back to Menu Button
    public void Script2Close()
    {
        menu.SetActive(true);
        script2.SetActive(false);
    }

    // Script1 Page Up Button
    public List<GameObject> script2Pages = new List<GameObject>();
    public void Script2PageUp()
    {
        int curr = 0;

        for (int i = 0; i < script2Pages.Count; i++)
        {
            if (script2Pages[i].activeSelf == true)
            {
                curr = i;
            }
        }

        if (curr != (script2Pages.Count - 1))
        {
            script2Pages[curr].SetActive(false);
            script2Pages[curr + 1].SetActive(true);
        }
        else
        {
            return;
        }
    }

    // Script1 Page Down Button
    public void Script2PageDown()
    {
        int curr = 0;

        for (int i = 0; i < script2Pages.Count; i++)
        {
            if (script2Pages[i].activeSelf == true)
            {
                curr = i;
            }
        }

        if (curr != 0)
        {
            script2Pages[curr].SetActive(false);
            script2Pages[curr - 1].SetActive(true);
        }
        else
        {
            return;
        }
    }

    // [     Script3     ]
    // Script1 Select Button
    [Header("[ Script 3 ]")]
    public GameObject script3;
    public void Script3Open()
    {
        menu.SetActive(false);
        script3.SetActive(true);
    }

    // Script1 Back to Menu Button
    public void Script3Close()
    {
        menu.SetActive(true);
        script3.SetActive(false);
    }

    // Script1 Page Up Button
    public List<GameObject> script3Pages = new List<GameObject>();
    public void Script3PageUp()
    {
        int curr = 0;

        for (int i = 0; i < script3Pages.Count; i++)
        {
            if (script3Pages[i].activeSelf == true)
            {
                curr = i;
            }
        }

        if (curr != (script3Pages.Count - 1))
        {
            script3Pages[curr].SetActive(false);
            script3Pages[curr + 1].SetActive(true);
        }
        else
        {
            return;
        }
    }

    // Script1 Page Down Button
    public void Script3PageDown()
    {
        int curr = 0;

        for (int i = 0; i < script3Pages.Count; i++)
        {
            if (script3Pages[i].activeSelf == true)
            {
                curr = i;
            }
        }

        if (curr != 0)
        {
            script3Pages[curr].SetActive(false);
            script3Pages[curr - 1].SetActive(true);
        }
        else
        {
            return;
        }
    }
}

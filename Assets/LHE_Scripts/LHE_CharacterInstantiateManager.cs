using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LHE_CharacterInstantiateManager : MonoBehaviour
{
    public static LHE_CharacterInstantiateManager Instance;
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

    public Dropdown characterDropdown;
    public int characterNum;

    // Start is called before the first frame update
    void Start()
    {
        //print("ĳ���� " + characterDropdown.value); // 0
    }

    // Update is called once per frame
    void Update()
    {
        //print("ĳ���� " + characterDropdown.value); // 1
    }

    public void SelectCharacter()
    {
        characterNum = characterDropdown.value;
    } 
}

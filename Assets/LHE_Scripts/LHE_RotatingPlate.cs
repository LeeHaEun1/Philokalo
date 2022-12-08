using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [원심분리기]
// 플레이어가 트리거에 들어오면
// (상호작용 가능성을 알리는 UI를 띄우고)
// 플레이어가 X버튼을 누르면 자리를 배정해준다
// 플레이어가 Space바를 누르면 원심분리기 회전(시간에 비례해서 회전력 감소하도록??)
public class LHE_RotatingPlate : MonoBehaviour
{
    public bool canInteract = false;
    public bool onPoint = false;

    public Transform point1;
    public Transform point2;
    public Transform point3;

    Rigidbody rb;

    public List<GameObject> player = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Rotate()
    {
        rb.AddTorque(transform.up * 30, ForceMode.Impulse);
        player[0].transform.position = point1.position;
        player[0].transform.rotation = point1.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(player.Contains(other.gameObject) == false)
            {
                player.Add(other.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [���ɺи���]
// �÷��̾ Ʈ���ſ� ������
// (��ȣ�ۿ� ���ɼ��� �˸��� UI�� ����)
// �÷��̾ X��ư�� ������ �ڸ��� �������ش�
// �÷��̾ Space�ٸ� ������ ���ɺи��� ȸ��(�ð��� ����ؼ� ȸ���� �����ϵ���??)
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

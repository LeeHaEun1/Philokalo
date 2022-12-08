using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class LHE_CharacterMove : MonoBehaviourPun, IPunObservable
{
    CharacterController cc;
    Animator anim;

    [Header("Camera")]
    public Transform cam;

    [Header("Move")]
    public float moveSpeed = 6;

    [Header("Turn")]
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //[Header("Jump")]
    public float gravity = -20;
    float yVelocity;
    public float jumpPower = 10;
    //public bool isJumping = false;
    ////int countJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            transform.Find("FaceCamera").gameObject.SetActive(false);
        }
        //if (photonView.IsMine)
        //{
            cc = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();

            // ĳ���� photon �������� ���� ���� �Ҵ�
            cam = Camera.main.transform;
        //}
        //else
        //{
        //    return;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // PhotonView = isMine && ���� ��ȣ�ۿ� ������ ä���г��� ��Ŀ���Ǿ� ���� �ʴٸ� Move
        if (photonView.IsMine && !LHE_CurrentInteractableChatManager.Instance.isTextChatFocused)
        {
            yVelocity += gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpPower;
            }

            // [Move]
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 dir = new Vector3(h, 0, v).normalized;

            if (dir.magnitude >= 0.1f || Input.GetButtonDown("Jump"))
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothTime, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, targetAngle, 0);

                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                // LHE
                moveDir.Normalize();

                moveDir.y = yVelocity;

                cc.Move(moveDir * moveSpeed * Time.deltaTime);
                //cc.SimpleMove(moveDir.normalized * moveSpeed);

                anim.SetFloat("dir", dir.magnitude);

                // Animation Sync
                photonView.RPC(nameof(RpcSetFloat), RpcTarget.All, "dir", dir.magnitude);
            }
            else
            {
                anim.SetFloat("dir", dir.magnitude);

                // Animation Sync
                photonView.RPC(nameof(RpcSetFloat), RpcTarget.All, "dir", dir.magnitude);
            }
        }
        else
        {
            return;
        }
    }

    // IPunObservable �������̽� ����
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ������ ������
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        // ������ �ޱ�
        else if (stream.IsReading)
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    // Animation Sync RPC �Լ�
    [PunRPC]
    void RpcSetFloat(string trigger, float f)
    {
        // null reference exception
        // anim�� ��ã�°ǰ�..?

        // ���� ���������� ��� �������ٰ� start������ photonview.ismine�����ִ� ó���� �� �� ���� ����, �ִϸ��̼� ����ȭ�ȴ�
        // => ó���� �� ���� �� ���°���..??

        anim.SetFloat(trigger, f);
    }
}

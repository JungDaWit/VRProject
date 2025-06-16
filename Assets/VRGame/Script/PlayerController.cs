using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public static int Hp = 5;

    public AudioClip JumpSound;
    public AudioClip MoveSound;
    private AudioSource audioSource;

    private Rigidbody _rigid;
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;
    public float jumpPower = 5f;
    private bool IsFloor;

    public List<GameObject> PlayerHp;
     
    private float xRotation = 0f;

    public float soundInterval = 0.3f;
    private float lastSoundTime = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _rigid = GetComponent<Rigidbody>();
       // Cursor.lockState = CursorLockMode.Locked;
        Hp = 5;
    }

    void Update()
    {
        RotateView(); 
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && IsFloor)
        {
            Jump();
        }

    }

    void RotateView()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
       

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


         //transform.Rotate(Vector3.up * mouseX);
        transform.Rotate(Vector3.up * (mouseX + Input.GetAxis("rightStick") * rotationSpeed * Time.deltaTime));
       // float rightStick = Input.GetAxis("rightStick") * rotationSpeed * Time.deltaTime;


    }
    private void Jump()
    {
            Debug.Log("점프 시도!");
            _rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        audioSource.PlayOneShot(JumpSound);
        IsFloor = false;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("floor"))
        {
           // Debug.Log("바닥 활성");
            IsFloor = true;
        }
    }

    private void MovePlayer()
    {
       

       // float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.forward * v;

        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
      if((Input.GetKey(KeyCode.W) ||
          Input.GetKey(KeyCode.A) ||
          Input.GetKey(KeyCode.S) ||
          Input.GetKey(KeyCode.D)) && IsFloor && Time.time >= lastSoundTime + soundInterval)
         {
            audioSource.PlayOneShot(MoveSound);
            lastSoundTime = Time.time;

         }

    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Hp --;
            if (Hp == 4)
            {
                GameManager.Instance.OnPlayerHp4.Invoke();
                Debug.Log("플레이어체력감소");
            }
            else if (Hp == 3)
                GameManager.Instance.OnPlayerHp3.Invoke();
            else if (Hp == 2)
                GameManager.Instance.OnPlayerHp2.Invoke();
            else if (Hp == 1)
                GameManager.Instance.OnPlayerHp1.Invoke();
            else if (Hp == 0)
            {
                
                GameManager.Instance.DIe.Invoke();
                
            }
        }
        else if (other.CompareTag("magma"))
        {
            GameManager.Instance.DIe.Invoke();
        }
        else if (other.CompareTag("Clear"))
        {
            GameManager.Instance.Clear.Invoke();
        }
    }
    
    

}

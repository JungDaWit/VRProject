using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
//using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent OnPlayerHp1 = new UnityEvent();
    public UnityEvent OnPlayerHp2 = new UnityEvent();
    public UnityEvent OnPlayerHp3 = new UnityEvent();
    public UnityEvent OnPlayerHp4 = new UnityEvent();
    public UnityEvent DIe = new UnityEvent();
    public UnityEvent Clear = new UnityEvent();

    private float cachedClearTime = 0;

    [SerializeField] HpEffect hpEffectController;

    [Header("UI")]
    [SerializeField] GameObject Map;
    [SerializeField] GameObject SettingUi;
    [SerializeField] GameObject HpImgae1;
    [SerializeField] GameObject HpImgae2;
    [SerializeField] GameObject HpImgae3;
    [SerializeField] GameObject HpImgae4;
    [SerializeField] GameObject HpImgae5;

    public AudioClip hitSound;
    private AudioSource audioSource;

    public AudioClip heartSound;



    private void Awake()
    {
        if (Instance == null) 
       {
            
           Instance = this;
            Resources.Load<GameObject>("GameManager");
            //DontDestroyOnLoad(gameObject);
           

       }
        else
        {
            Destroy(gameObject);
        }
       OnEnable();
        
    }
   
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        HpImgae1.SetActive(true);
        HpImgae2.SetActive(true);
        HpImgae3.SetActive(true);
        HpImgae4.SetActive(true);
        HpImgae5.SetActive(true);


    }
    private void Update()
    {
        MiniMap();
        setting();
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            cachedClearTime = timer.GetClearTime();
        }
    }
    private void MiniMap()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Map.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            Map.SetActive(false);
        }
    }
    private void setting()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SettingUi.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            SettingUi.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void OnEnable()
    {
        
        OnPlayerHp4.AddListener(PlayerHp4);
        OnPlayerHp3.AddListener(PlayerHp3);
        OnPlayerHp2.AddListener(PlayerHp2);
        OnPlayerHp1.AddListener(PlayerHp1);
        DIe.AddListener(GameOver);
        Clear.AddListener(GameClear);
    }
    private void PlayerHp4()
    {
        Debug.Log("플레이어체력감소");
        HpImgae5.SetActive(false);
        audioSource.PlayOneShot(hitSound);
        hpEffectController.SetIntensity(0.1f);
    }
    private void PlayerHp3()
    {
        HpImgae4.SetActive(false);
        audioSource.PlayOneShot(hitSound);
        hpEffectController.SetIntensity(0.2f);
    }
    private void PlayerHp2()
    {
        HpImgae3.SetActive(false);
        audioSource.PlayOneShot(hitSound);
        hpEffectController.SetIntensity(0.3f);

        audioSource.clip = heartSound;
        audioSource.loop = true;
        audioSource.Play();
    }
    private void PlayerHp1()
    {
        HpImgae2.SetActive(false);
        audioSource.PlayOneShot(hitSound);
        hpEffectController.SetIntensity(0.4f);


    }
    private void GameOver()
    {
        SceneManager.LoadScene("InGame");
        Cursor.lockState = CursorLockMode.None;
    }
    private void GameClear()
    {
        StartCoroutine(HandleGameClear());
        //  Timer timer = FindObjectOfType<Timer>();
        //
        //  Debug.Log("플레이 타임 저장: " + cachedClearTime);
        //  PlayerPrefs.SetFloat("LastClearTime", cachedClearTime);
        //  PlayerPrefs.Save();
        //
        //  SceneManager.LoadScene("GameClear");
        //  Cursor.lockState = CursorLockMode.None;
    }
    private IEnumerator HandleGameClear()
    {
        Timer timer = FindObjectOfType<Timer>();
        if(timer == null)
        {
            yield break;
        }
        float clearTime = timer.GetClearTime();

        PlayerPrefs.SetFloat("LastClearTime", clearTime);

        float bestTime = PlayerPrefs.GetFloat("BestClearTime", float.MaxValue);
        if(clearTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestClearTime", clearTime);
        }
        PlayerPrefs.Save();
        yield return null;

        SceneManager.LoadScene("InGame");
        Cursor.lockState = CursorLockMode.None;
        
    }
}

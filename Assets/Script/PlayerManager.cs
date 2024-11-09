using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    //playerのライフの設定
    public GameObject player;
    //playerHPの数値の変数宣言
    public int playerLife = 3;
    //playerのRigidbody2Dコンポーネントを取得する変数を宣言
    private Rigidbody2D rb;
    //ノックバック時の力を宣言
    public float backForce = 20.0f;
    //点滅させるためRendererコンポーネントを格納する変数宣言を取得
    [SerializeField] private Renderer ren;
    //ダメージを受けたときの点滅周期の秒数を宣言
    public float blink = 1;
    //タイマーの宣言
    private float timer;
    //Damageの判定
    public bool isDamage = false;
    Vector2 velo;
    private PlayerController playerController;
    private Memberscript ms;
    private bool member;
    private GameObject Member;
    public AudioSource audioSource;
    public AudioClip damageSound;
    private int memberCount;
    public Text clearText;
    public Text gameOverText;
    public GameManager gameManager;
    public AudioClip rescueAudio;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = player.GetComponent<AudioSource>();
        //playerのRigidbody2Dコンポーネントを取得
        rb = player.GetComponent<Rigidbody2D>();
        //playerのRendererコンポーネントを取得
        ren = player.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //点滅アニメーション
        playerController = player.GetComponent<PlayerController>();
        timer += Time.deltaTime;
        double repeatValue = Math.Sin(blink * 10 * timer);
        //ダメージを受けている場合の点滅操作
        if(isDamage){
            if(repeatValue >= 0){
                ren.enabled = true;
            }
            else{
                ren.enabled = false;
            }
        }
        else{
            ren.enabled = true;
        }

        if(memberCount >= 3)
        {
            GameClear();
        }
    }
    
    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Damage" || other.gameObject.tag == "Enemy"){
            if(!isDamage){
                playerLife--;
            }
            StartCoroutine(Damage());
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Damage") || other.gameObject.CompareTag("Enemy")){
            if(!isDamage){
                playerLife--;
                audioSource.PlayOneShot(damageSound);
            }
            StartCoroutine(Damage());
        }
        //msにMemberscriptコンポーネンopトを取得
        if(other.gameObject.CompareTag("Member")){
            ms = other.GetComponent<Memberscript>();
            member = ms.isMember;
            Member = other.gameObject;
        }

        //メンバーを救ってかつゴールに触れたら，救った判定をする．
        if(other.gameObject.CompareTag("Goal") && member){
            Destroy(Member.gameObject);
            member = false;
            memberCount++;
            audioSource.PlayOneShot(rescueAudio);
        }

        if(other.gameObject.CompareTag("Fall"))
        {
            gameManager.StartCoroutine("FeadOut");
        }
    }

    //ダメージを受けたときの動作
    IEnumerator Damage(){
        //時間を数える
        isDamage = true;
        playerController = player.GetComponent<PlayerController>();
        rb = player.GetComponent<Rigidbody2D>();
        velo = rb.velocity;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.01f);
        //ノックバックのコード
        if(playerController.horizontalInput >= 0){
            rb.velocity = new Vector2(-backForce, backForce);
        }
        else if(playerController.horizontalInput < 0){
            rb.velocity = new Vector2(backForce, backForce);
        }
        yield return new WaitForSeconds(1);
        ren.enabled = true;
        isDamage = false;
    }

    public void GameClear(){
        SceneManager.LoadScene("GameClear");
    }
}

using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{

    //パブリックオブジェクトは使用せず，privaate使う


    //プレイヤーオブジェクトの取得
    public GameObject player;
    //プレイヤーの位置の取得
    public Vector3 playerPosition;
    //移動速度の設定
    private float speed;
    //Rigidbodyの取得
    public Rigidbody2D rb;
    //ジャンプ力の設定
    public float jumpForce = 10f;
    //他のゲームオブジェクトの当たり判定
    public bool isOther = true;
    //最大連続ジャンプ回数の設定
    public int maxJumpCount;
    //ジャンプ回数の設定
    private int jumpCount;
    public bool isJump = true;
    //ジャンプしているときの移動速度の数値変数宣言
    public float jumpSpeed;
    //歩くスピードの数値変数宣言
    public float walkSpeed;
    //梯子を上るスピードの数値変数宣言
    public float ladderSpeed;
    //梯子上るときのベクトル
    public Vector3 playerLadderPosition;
    //梯子を上る速さの数値
    public Vector3 ladderForce;
    //縦移動の数値を取得
    float verticalInput;
    //Animatorコンポーネントの取得
    public Animator animator;
    //横移動の数値を取得
    public float horizontalInput;
    //PlayerManagerの取得
    public PlayerManager pm;
    public GameObject beam;
    private ShotScript ss;
    private float gunTimer = 1;
    public AudioClip gunSound;
    public AudioClip jumpSound;
    private AudioSource audioSource;

    public Slider slider;
    // Start is called before th;e first frame update
    void Start()
    {
        //ジャンプしていない判定
        isJump = false;
        animator.SetBool("Jump", isJump);
        //プレイヤーのRigidbodyの取得
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
{
    gunTimer += Time.deltaTime;
    ss = beam.GetComponent<ShotScript>();
    pm = player.GetComponent<PlayerManager>();
    //移動方向の数値を取得
    if(pm.isDamage == false){
        horizontalInput = Input.GetAxis("Horizontal");
        //もし数値が0より小さければ，キャラクターアニメーションを左右反転させる
    if(horizontalInput < 0){
        Vector3 scale = player.transform.localScale;
        scale.x = -3;
        player.transform.localScale = scale;
    }

    //違う場合，そのままのアニメーションになる．
    else if(horizontalInput > 0){
        Vector3 scale = player.transform.localScale;
        scale.x = 3;
        player.transform.localScale = scale;
    }

    //AddForceを使用する

    //Rigidbodyに速度ベクトルを定義する．
    rb.velocity = new Vector2(horizontalInput * walkSpeed, rb.velocity.y);

    //歩行アニメーションの再生
    animator.SetFloat("Horizontal", horizontalInput);

    //ジャンプの実行
    if(Input.GetButtonDown("Jump") && jumpCount < maxJumpCount){
        rb.AddForce(Vector2.up * jumpForce * 65); //上方向に力を加えてジャンプさせる
        jumpCount++; //ジャンプカウントの加算
        isJump = true; //ジャンプしている判定
        animator.SetBool("Jump", isJump); //ジャンプしていればジャンプアニメーションを再生
        audioSource.PlayOneShot(jumpSound);
    }

    if(Input.GetKeyUp(KeyCode.UpArrow)){
            verticalInput = 0;
            animator.SetFloat("Vertical", verticalInput);
        }
    }
    slider.value = gunTimer;

    if(gunTimer >= 1){
        if(Input.GetKeyDown(KeyCode.Return)){
            shot();
            gunTimer = 0;
        }
    }
}

    //梯子を上るスクリプト
    public void Ladder(){
        if(Input.GetKey(KeyCode.UpArrow)){
            verticalInput = Input.GetAxis("Vertical");
            animator.SetFloat("Vertical", verticalInput);
            rb.velocity = new Vector3(horizontalInput, verticalInput * ladderSpeed, 0);
        }
    }

    //地面に接触したときのスクリプト
    public void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            jumpCount = 0;
            speed = jumpSpeed;
            isJump = false;
            animator.SetBool("Jump", isJump);
        }
    }

    public void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "Ladder"){
            Ladder();
        }
    }

    public void shot(){
        Instantiate(beam, transform.position + new Vector3(0, 8, 0), Quaternion.identity);
        ss.player = this.player.gameObject;
        audioSource.PlayOneShot(gunSound);
    }
}

using UnityEngine;

public class Memberscript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Animator ani;
    public bool isMember = false;
    [SerializeField]
    public float followSpeed = 2.0f;  // 追従速度を追加
    private PlayerController pc; // 初期値を false に変更
    private bool isGround;
    private Vector3 defaultscale;
    private Vector3 scales;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        defaultscale = transform.localScale;
        scale = defaultscale.x;
        //pc = player.GetComponent<PlayerController>();
        ani = GetComponent<Animator>();
        ani.Play(ani.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {

        if (isMember && player != null)
        {
            // 現在の位置からプレイヤーの位置へスムーズに移動
            transform.position = Vector3.Lerp(transform.position, player.transform.position, followSpeed * Time.deltaTime);
            //transform.parent = player.transform;
        }
        pc = player.GetComponent<PlayerController>();
        isGround = pc.isJump;
        ani.SetBool("isGround", !isGround);
        if(player.transform.position.x > transform.position.x){
            scales = defaultscale;
            transform.localScale = scales;
        }
        else{
            scales = defaultscale;
            scales.x = -scale;
            transform.localScale = scales;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // OnTriggerEnter2D を OnCollisionEnter2D に変更
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isMember = true;
        }
        if(other.gameObject.CompareTag("Ground"))
        {

        }
    }

    public void OnTriggerExit2D(Collider2D other) // OnCollisionExit2D を OnTriggerEnter2D に変更
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            //ani.SetBool("isGround", isGround);
            Debug.Log("ddd");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //敵のゲームオブジェクトの変数宣言
    public GameObject enemy;
    //敵の動き方のベクトルを宣言
    public Vector3 enemyMove;
    public Vector3 scale;
    private float scaleX;
    private float timer;
    private float rand;
    public float speed;
    private float blink = 80;
    private float repeatValue;
    private Renderer ren;
    private bool isDeath = false;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(1, 3);
        enemyMove = enemy.transform.position;
        scale = enemy.transform.localScale;
        scaleX = scale.x;
        ren = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //タイマーの生成
        timer += Time.deltaTime;
        //もしsin関数が0以下ならマイナス方向に動く
        if(0 >= Mathf.Sin(timer * rand)){
            enemyMove.x -= speed * Time.deltaTime;
            enemy.transform.position = enemyMove;
            scale.x = -scaleX;
            enemy.transform.localScale = scale;
        }
        //もしsin関数が0以上ならプラス方向に動く
        else{
            enemyMove.x += speed * Time.deltaTime;
            enemy.transform.position = enemyMove;
            scale.x = scaleX;
            enemy.transform.localScale = scale;
        }
        if(isDeath){
            double repeatValue = Mathf.Sin(blink * 10 * timer);
            if(repeatValue >= 0){
                ren.enabled = true;
            }
            else{
                ren.enabled = false;
            }
            StartCoroutine(Death());
        
        }
    }

    public void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.CompareTag("Gun")){
            isDeath = true;
        }
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

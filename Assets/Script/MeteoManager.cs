using UnityEngine;

public class MeteoManager : MonoBehaviour
{
    //メテオを生成する間隔
    public float meteoDelay;
    public GameObject meteo;
    BoxCollider2D col;

    private float time;
    private Rigidbody2D rb;
    // Update is called once per frame
    void Start(){
    }
    void Update()
    {
        time += Time.deltaTime;
        if(time >= meteoDelay){
            shot();
            time = 0;
        }
    }

    public void shot(){
        Instantiate(meteo, transform.position, Quaternion.identity);
    }
}

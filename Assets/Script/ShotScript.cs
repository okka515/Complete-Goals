using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotScript : MonoBehaviour
{
    public GameObject player;
    private Vector2 pos;
    [SerializeField] private float speed;
    private float playerForward;
    private PlayerController pc;
    private Vector2 beamScale;
    // Start is called before the first frame update
    void Start()
    {
        playerForward = player.transform.localScale.x;
        if(playerForward >= 0){
            beamScale= transform.localScale;
            transform.localScale = beamScale;
        }
        else{
             beamScale = transform.localScale;
            beamScale.x = -beamScale.x;
            transform.localScale = beamScale;
        }

        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {   
        if(playerForward >= 0){
            pos = transform.position;
            pos.x += speed * Time.deltaTime;
            transform.position = pos;
            
        }

        else if(playerForward < 0){
            pos = transform.position;
            pos.x -= speed * Time.deltaTime;
            transform.position = pos;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }
}

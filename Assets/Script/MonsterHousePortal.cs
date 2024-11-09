using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHousePortal : MonoBehaviour
{
    //ポータルの入口のゲームオブジェクトを取得
    public GameObject portalIn;
    //ポータルの出口のゲームオブジェクトを取得
    public GameObject portalOut;
    public GameObject player;
        public void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            player.transform.position = portalOut.transform.position;
        }
    }
}

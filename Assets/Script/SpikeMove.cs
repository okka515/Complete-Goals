using System;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeMove : MonoBehaviour
{
    //Spikeの動くスピードの数値
    public float moveSpeed;
    public GameObject spike;
    public Transform spikeTrans;
    //Spikeの最初の場所の取得
    public Vector3 defaultSpikePosition;
    //Spikeの位置を取得する変数の宣言
    public Vector3 spikePosition;
    //Spikeの振れ幅の指定
    public float moveScope;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        //最初のSpikeの位置を取得
        defaultSpikePosition = spike.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        spikePosition = spikeTrans.position;
        //動いている範囲から，動き方を変える条件分岐
        spikePosition.x = (float)(defaultSpikePosition.x + moveScope * Math.Sin(moveSpeed * time));
        spikeTrans.position = spikePosition;
    }
}

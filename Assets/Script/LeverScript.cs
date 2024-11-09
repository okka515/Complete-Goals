using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    public GameObject floor;
    public GameObject levarUp;
    public GameObject levarDown;
    
    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            levarDown.SetActive(false);
            levarUp.SetActive(true);
            floor.SetActive(false);
        }
    }

   
}

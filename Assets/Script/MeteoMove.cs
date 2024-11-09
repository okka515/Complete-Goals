using UnityEngine;
using UnityEngine.AI;

public class MeteoMove : MonoBehaviour
{
    Vector3 moveDirection;
    public float moveSpeed = 100.0f;
    Rigidbody2D fireBallRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        fireBallRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        moveDirection = new Vector3(-1, 0, 0);
        fireBallRigidbody.velocity = moveDirection;
        fireBallRigidbody.velocity *= moveSpeed;
        Destroy(gameObject, 15f);
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
    }
}

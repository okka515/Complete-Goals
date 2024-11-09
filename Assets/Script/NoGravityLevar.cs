using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoGravityLevar : MonoBehaviour
{
    public GameObject player;
    public GameObject levarUp;
    public GameObject levarDown;
    public Rigidbody2D rb;
    private float defaultGravity;
    private float defaultJumpForce;
    private float gravityDuration = 5.0f;
    private float gravityTimer;
    public Image gage;
    private bool startTimer = false;
    public AudioSource audioSource;
    public AudioClip timerAudio;

    PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = player.GetComponent<PlayerController>();
        rb = player.GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
        defaultJumpForce = pc.jumpForce;
        gage.fillAmount = 0;
        gage.enabled = false;
        audioSource.clip = timerAudio;
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer)
        {
            gravityTimer -= Time.deltaTime;
            gage.fillAmount = gravityTimer / gravityDuration;

            if (gravityTimer <= 0)
            {
                StopCoroutine("NoGravity");
                ResetGravity();
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            levarDown.SetActive(false);
            levarUp.SetActive(true);
            StartCoroutine("NoGravity");
        }
    }

    IEnumerator NoGravity()
    {
        startTimer = true;
        gravityTimer = gravityDuration;
        gage.enabled = true;
        rb.gravityScale = 1f;
        audioSource.Play();

        while (gravityTimer > 0)
        {
            yield return null;
        }

        ResetGravity();
    }

    private void ResetGravity()
    {
        rb.gravityScale = defaultGravity;
        startTimer = false;
        gage.fillAmount = 0;
        gage.enabled = false;
        audioSource.Stop();
    }
}

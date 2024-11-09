using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public GameObject spaceship;
    public Vector3 spaceshipPosition;
    public float speed = 10.0f;

    public Text gameClearText;
    public float feadTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        spaceshipPosition = spaceship.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        spaceshipPosition.x += Time.deltaTime * speed;
        spaceship.transform.position = spaceshipPosition;
        if(spaceshipPosition.x > 10){
            StartCoroutine(FeadIn());
        }
    }

    IEnumerator FeadIn()
    {
        gameClearText.gameObject.SetActive(true);
        float panelTime = 0.0f;
        Color startColor = gameClearText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);
        while (panelTime < feadTime)
        {
            panelTime += Time.deltaTime;
            float t = Mathf.Clamp01(panelTime / feadTime);
            gameClearText.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        gameClearText.color = Color.black;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Title");
    }
}

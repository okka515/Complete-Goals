using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerPosition;
    public float rotateSpeed;
    public float moveSpeed;
    public Text gameOverText;
    public float feadTime;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // z軸周りの回転
        float rotationZ = Time.deltaTime * rotateSpeed;
        player.transform.Rotate(0, 0, rotationZ);

        // x方向への移動
        playerPosition.x += moveSpeed * Time.deltaTime;
        player.transform.position = playerPosition;
        if(playerPosition.x > 10)
        {
            StartCoroutine(FeadIn());
        }
    }

     IEnumerator FeadIn()
    {
        gameOverText.gameObject.SetActive(true);
        float panelTime = 0.0f;
        Color startColor = gameOverText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);
        while (panelTime < feadTime)
        {
            panelTime += Time.deltaTime;
            float t = Mathf.Clamp01(panelTime / feadTime);
            gameOverText.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        gameOverText.color = Color.black;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Title");
    }
}

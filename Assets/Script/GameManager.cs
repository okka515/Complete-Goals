using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // PlayerManagerの取得
    PlayerManager pm;
    // Playerの取得
    public GameObject player;
    public float spaceShipTime = 60.0f;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public int blinking;
    private float timer;
    public Image feadPanel;
    public float feadTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        pm = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.playerLife == 2 && pm.isDamage)
        {
            StartCoroutine(Blink(heart3));
        }
        else if (pm.playerLife == 1 && pm.isDamage)
        {
            StartCoroutine(Blink(heart2));
        }
        else if (pm.playerLife == 0 && pm.isDamage)
        {
            StartCoroutine(Blink(heart1));
        }
    }

    IEnumerator Blink(GameObject heart)
    {
        Renderer ren = heart.GetComponent<Renderer>();
        timer += Time.deltaTime;
        float blinkJudg = Mathf.Sin(timer * blinking);
        if (blinkJudg >= 0)
        {
            ren.enabled = true;
        }
        else
        {
            ren.enabled = false;
        }
        yield return new WaitForSeconds(2.1f);
        Destroy(heart);
        if (pm.playerLife <= 0)
        {
            StartCoroutine(FeadOut());
        }
    }

    IEnumerator FeadOut()
    {
        feadPanel.gameObject.SetActive(true); // フェードパネルを有効にする
        float panelTime = 0.0f;
        Color startColor = feadPanel.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);
        while (panelTime < feadTime)
        {
            panelTime += Time.deltaTime;
            float t = Mathf.Clamp01(panelTime / feadTime);
            feadPanel.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        feadPanel.color = Color.black;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("GameOver");
    }
}

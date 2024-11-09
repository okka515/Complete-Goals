using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class textManager : MonoBehaviour
{
    public Text controll;
    public Text controlls;
    public Text world;
    public Text worlds;
    public float enterInput;
    // Start is called before the first frame update
    void Start()
    {
        controll.gameObject.SetActive(true);
        controlls.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            enterInput++;
        }

        if(enterInput == 1)
        {
            controll.gameObject.SetActive(false);
            controlls.gameObject.SetActive(false);
            world.gameObject.SetActive(true);
            worlds.gameObject.SetActive(true);
        }
        if(enterInput == 2)
        {
            SceneManager.LoadScene("Title");
        }
    }
}

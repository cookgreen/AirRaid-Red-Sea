using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnClickStartSPGame()
    {
        SceneManager.LoadScene("SPGame");
    }

    public void OnClickCredit()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

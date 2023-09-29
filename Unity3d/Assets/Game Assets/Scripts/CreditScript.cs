using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
    private float currentTime;
    private GameObject CanvasPanel;
    private GameObject panelCreditText1;
    private GameObject panelCreditText2;
    private GameObject panelCreditText3;

    // Start is called before the first frame update
    void Start()
    {
        CanvasPanel = GameObject.Find("Canvas").transform.Find("Panel").transform.Find("Panel").gameObject;
        panelCreditText1 = CanvasPanel.transform.Find("panelCreditText1").gameObject;
        panelCreditText2 = CanvasPanel.transform.Find("panelCreditText2").gameObject;
        panelCreditText3 = CanvasPanel.transform.Find("panelCreditText3").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || currentTime >= 500)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (currentTime >= 0 && currentTime <= 150)
        {
            panelCreditText1.SetActive(true);
        }
        else if (currentTime > 150 && currentTime <= 300)
        {
            panelCreditText1.SetActive(false);
            panelCreditText2.SetActive(true);
        }
        else
        {
            panelCreditText2.SetActive(false);
            panelCreditText3.SetActive(true);

            panelCreditText3.transform.position = new Vector3(
                panelCreditText3.transform.position.x, 
                panelCreditText3.transform.position.y + 0.5f);
        }

        Debug.Log(currentTime);

        currentTime += 0.1f;
    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject OptionPanel;
    public GameObject CreditPanel;

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        OptionPanel.SetActive(false);
        CreditPanel.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
    }
     
    public void PlayButtonClicked()
    {
        Application.LoadLevel("Game1");
    }

    public void OptionButtonClicked()
    {
        MenuPanel.SetActive(false);
        OptionPanel.SetActive(true);
        CreditPanel.SetActive(false);
    }

    public void CreditButtonClicked()
    {
        MenuPanel.SetActive(false);
        OptionPanel.SetActive(false);
        CreditPanel.SetActive(true);
    }

    public void Quit_Clicked()
    {
        Application.Quit();
    }

    public void BackButtonClicked()
    {
        MenuPanel.SetActive(true);
        OptionPanel.SetActive(false);
        CreditPanel.SetActive(false);
    }
    
}

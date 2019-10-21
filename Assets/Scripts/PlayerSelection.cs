using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private GameObject playerSelectionObject;
    [SerializeField] private float selecrionObjyPos = 0;
    [SerializeField] private float leftObjectXPos = 0;
    [SerializeField] private float rightObjectXPos = 0;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject creditsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerMenuSelection();
    }

    public void ViewCredits()
    {
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }
}

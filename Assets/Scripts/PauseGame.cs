using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckPauseMenu();
        }
    }

    public void CheckPauseMenu()
    {
        if (PauseMenu.activeSelf)
        {
            UI_Manager.Instance.GoToMainMenu();
        }
        else if (!PauseMenu.activeSelf)
        {
            UI_Manager.Instance.GoToSettings();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{

    public Camera mainCamera; 
    public Camera characterCamera; 
    public GameObject Caractere;
    private GameManager gameManager;

    public void Awake()
    {
        characterCamera.enabled = false;
        Caractere.SetActive(false);
        gameManager = FindAnyObjectByType<GameManager>();
    }
    public void Switch()
    {

        if (mainCamera.enabled)
        {
            gameManager.TirdPerson = true;
            characterCamera.enabled = true;
            mainCamera.enabled = false;
            Caractere.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Caractere.transform.position = mainCamera.transform.position;
        }
        else
        {
            gameManager.TirdPerson = false;
            characterCamera.enabled = false;
            mainCamera.enabled = true;
            Caractere.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainCamera.transform.position = characterCamera.transform.position + new Vector3(0,2,0);
        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            Switch(); 
        }
    }
}

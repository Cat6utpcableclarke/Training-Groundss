using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menu : MonoBehaviour
{
    // Start is called before the first frame update

    public Canvas canvas;
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

     public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


     public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void Update()
    {
            if(Input.GetKey(KeyCode.Escape)){
                Cursor.lockState = CursorLockMode.None;
                canvas.gameObject.SetActive(true);
            };
    }


    // Update is called once per frame
    
}

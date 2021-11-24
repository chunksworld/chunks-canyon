using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    Transform player;
    public Transform GUI;
    public Transform Dead;
    GameObject cam;
    public TMP_Text currentPoints;

    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.GetComponent<CameraFollow>().enabled = true;
        InvokeRepeating("CheckIfAlive", 0f, 1f);
        Time.timeScale = 1f;

        GUI.gameObject.SetActive(true);
        Dead.gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        CONSTANTS.isAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ToMainMenu()
    {
        CONSTANTS.isAlive = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void CheckIfAlive()
    {
        if ( CONSTANTS.isAlive && Mathf.Abs(int.Parse(currentPoints.text) - player.transform.position.y) > 20f )
        {
            cam.GetComponent<CameraFollow>().enabled = false;
            wait1sec();
            CONSTANTS.isAlive = false;
            GUI.gameObject.SetActive(false);
            Dead.gameObject.SetActive(true);

            Time.timeScale = 0f;
        }

    }
     

    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(1f);
    }

}

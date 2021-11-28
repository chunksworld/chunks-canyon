using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]

    List<GameObject> levels = new List<GameObject>();
    int selectedLevel = 1;
    public void SelectLevel(int levelIndex)
    {
        for ( int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }
        levels[levelIndex].SetActive(true);
        selectedLevel = levelIndex + 1;
    }

    public void Play()
    {
        SceneManager.LoadScene(selectedLevel);
    }

}

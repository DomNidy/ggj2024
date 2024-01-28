using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(InitiateLevel);
    }

    private void InitiateLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

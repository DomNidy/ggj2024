using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BeginGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(InitiateGame);
    }

    private void InitiateGame()
    {
        SceneManager.LoadScene("IntroScene");
    }
}

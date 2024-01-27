using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{

    public FadeTip fadeTip;
    private bool shouldFadeOut;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shouldFadeOut = true;
        }
        if (shouldFadeOut) fadeTip.FadeTextOut();
    }
}

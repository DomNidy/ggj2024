using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeTip : MonoBehaviour
{

    public TMP_Text[] texts;
    public bool fadeInComplete = false;

    private void Awake()
    {
        texts = GetComponentsInChildren<TMP_Text>();
    }

    private void Start()
    {
        foreach (TMP_Text _text in texts)
        {
            _text.color = new Color(_text.color.r, _text.color.b, _text.color.g, 0);
        }
    }

    private void Update()
    {
        if (!fadeInComplete) FadeTextIn();
    }

    private void FadeTextIn()
    {
        foreach (TMP_Text _text in texts)
        {
            _text.color = new Color(
                _text.color.r,
                _text.color.b,
                _text.color.g,
                Mathf.Lerp(_text.color.a, 1f, Time.deltaTime / 3)
            );

            if (_text.color.a >= 0.9)
            {
                _text.color = new Color(_text.color.r, _text.color.b, _text.color.g, 1);
                fadeInComplete = true;
            }
        }
    }

    public void FadeTextOut()
    {
        Debug.Log("Fade text out");
        foreach (TMP_Text _text in texts)
        {
            _text.color = new Color(
                _text.color.r,
                _text.color.b,
                _text.color.g,
                Mathf.Lerp(_text.color.a, 0f, Time.deltaTime * 2)
            );

            gameObject.transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 3);

            Debug.Log(_text.color.a + " interpolating");
        }
    }
}

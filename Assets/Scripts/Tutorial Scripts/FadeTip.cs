using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeTip : MonoBehaviour
{

    public TMP_Text[] texts;
    public RawImage[] images;
    private bool beganFadeOut = false;
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

        foreach (RawImage _rawImage in images)
        {
            _rawImage.color = new Color(_rawImage.color.r, _rawImage.color.b, _rawImage.color.g, 0);
        }


        StartCoroutine(FadeTextIn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !beganFadeOut)
        {
            StopAllCoroutines();
            StartCoroutine(FadeTextOut());
        }
    }

    private IEnumerator FadeTextIn()
    {

        for (float alpha = 0f; alpha < 1; alpha += Time.deltaTime)
        {
            foreach (TMP_Text _text in texts)
            {
                _text.color = new Color(
                    _text.color.r,
                    _text.color.b,
                    _text.color.g,
                   alpha
                );

            }

            foreach (RawImage _rawImage in images)
            {
                _rawImage.color = new Color(_rawImage.color.r, _rawImage.color.b, _rawImage.color.g, alpha);

            }

            yield return null;
        }

        Debug.Log("Fade complete");
    }

    public IEnumerator FadeTextOut()
    {
        Debug.Log("Fade text out coroutine started");
        beganFadeOut = true;
        for (float alpha = 1f; alpha > 0; alpha -= Time.deltaTime)
        {
            foreach (TMP_Text _text in texts)
            {
                _text.color = new Color(
                    _text.color.r,
                    _text.color.b,
                    _text.color.g,
                   alpha
                );

            }

            foreach (RawImage _rawImage in images)
            {
                _rawImage.color = new Color(_rawImage.color.r, _rawImage.color.b, _rawImage.color.g, alpha);

            }

            // We are scaling the stuff down using the alpha as the y-scale
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 0, Time.deltaTime), alpha, 0);

            yield return null;
        }

        Destroy(this.gameObject);
        Debug.Log("Fade complete");
    }

}

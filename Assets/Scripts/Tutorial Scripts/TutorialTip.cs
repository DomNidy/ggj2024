using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTip : MonoBehaviour
{
    public GameObject tipPrefab;
    public FadeTip fadeTip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(tipPrefab, UIManager.Instance.GetUICanvas().transform);
        Destroy(this.gameObject);
    }
}

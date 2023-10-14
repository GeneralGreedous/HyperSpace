using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private TextMeshProUGUI punkty;

    private void Awake()
    {
        Instance = this;
    }
    public void UpdateScore()
    {
        punkty.text = "Score:" + GameManager.Instance.punkty.ToString();
    }

}

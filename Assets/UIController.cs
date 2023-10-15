using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private TextMeshProUGUI punkty;
    public float fps = 0;
    public bool countFPS = false;
    private float[] frameDeltaTimeArray;
    private int lastFrameIndex;

    [SerializeField] private Button _ButtonStartGame;
    private void Awake()
    {
        Instance = this;
        frameDeltaTimeArray = new float[50];
    }

    private void Start()
    {
        UpdateScore();
    }
    public void UpdateScore()
    {
        punkty.text = "Score:" + GameManager.Instance.punkty.ToString();
    }

    private void Update()
    {
        if (countFPS)
        {
            fpsCounter();
        }
    }

    private void fpsCounter()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
        float total = 0;
        foreach (var frame in frameDeltaTimeArray)
        {
            total += frame;
        }
        fps = frameDeltaTimeArray.Length / total;
    }

    public void startGame()
    {
        _ButtonStartGame.gameObject.SetActive(false);
        GameManager.Instance.startGame();
    }

}

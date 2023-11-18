using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private TextMeshProUGUI punkty;
    [SerializeField] private TextMeshProUGUI timeRemain;

    [SerializeField] private TextMeshProUGUI endText;

    public float fps = 0;
    public bool countFPS = false;
    private float[] frameDeltaTimeArray;
    private int lastFrameIndex;

    private int endScore;


    [SerializeField] private Button _ButtonStartGame;

    [SerializeField] private Button _ButtonReStartGame;
    private void Awake()
    {
        Instance = this;
        frameDeltaTimeArray = new float[50];
    }

    private void Start()
    {

        ShowEndScreens(false);
        UpdateScore();
    }
    public void UpdateScore()
    {
        punkty.text = "Score:" + GameManager.Instance.punkty.ToString();
    }

    public void UpdateTime(float time)
    {
        string myStringTime = time.ToString("0.00");
        timeRemain.text="Time \n"+ myStringTime;
    }

    public void UptadeEndTime(int points,int missed)
    {
        endText.text = $"score: {points} \n missed asteroids: {missed}";
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

    public void StartGame()
    {
        _ButtonStartGame.gameObject.SetActive(false);
        ShowEndScreens(false);
        GameManager.Instance.PreStartGame();
    }

    public void ShowEndScreens(bool act)
    {
        endText.gameObject.SetActive(act);
        _ButtonReStartGame.gameObject.SetActive(act);
    }


}

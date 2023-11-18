using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int punkty = 0;
    public int missedAsteroids = 0;

    private int endPoints;
    private int endMissedAsteroids;
    public bool playing = true;
    [SerializeField] private List<Asteroid> AsteroidsList;
    private Camera mainCamera;
    public Vector2 leftRight;
    public Vector2 downUp;

    [SerializeField] private float timeToPlay = 60f;

    private List<GameObject> asteroids;

    private Coroutine spawnAsteroidsHere;

    [SerializeField] private GameObject backGround;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        CameraFOV();
        useGUILayout = false;
        backGround.SetActive(true);
    }

    private void CameraFOV()
    {
        Vector3 leftDown = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 10));
        Vector3 rightUp = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 10));
        leftRight = new Vector2(leftDown.x, rightUp.x);
        downUp = new Vector2(leftDown.y, rightUp.y);
    }
    public void PreStartGame()
    {
        asteroids = new List<GameObject>();
        PlayerController.instance.PreStartPlayer();
    }
    public void StartGame()
    {
        spawnAsteroidsHere = StartCoroutine(SpawnAsteroids());
        StartCoroutine(StartTime());
        PlayerController.instance.StartPlayer();

    }

    public void RemoveAsterois(GameObject asteroidd)
    {
        if (asteroids.Contains(asteroidd))
        {
            asteroids.Remove(asteroidd);
            // Optionally, you might also want to destroy gaobj1 if you no longer need it

        }
    }
    IEnumerator SpawnAsteroids()
    {

        float left = Mathf.Lerp(leftRight.x, leftRight.y, 0.05f);
        float right = Mathf.Lerp(leftRight.x, leftRight.y, 0.95f);
        while (playing)
        {
            for (int i = 0; i < Random.Range(2, 8); i++)
            {
                SpawnAsteroid(left, right);
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }

    }

    IEnumerator StartTime()
    {
        float time = timeToPlay;
        while (time > 0)
        {
            time -= Time.deltaTime;
            UIController.Instance.UpdateTime(time);
            yield return new WaitForEndOfFrame();
        }

        time = 0f;
        UIController.Instance.UpdateTime(time);

        endPoints = punkty;
        endMissedAsteroids = missedAsteroids;
        punkty = 0;
        missedAsteroids = 0;
        SaveHighScore(endPoints);
        UIController.Instance.UptadeEndTime(endPoints, endMissedAsteroids, GetHighScore());
        StopsCoroutines();

        yield return new WaitForFixedUpdate();


    }

    private void SpawnAsteroid(float left, float right)
    {
        int randomNumber = Random.Range(0, AsteroidsList.Count);
        float randomNumber2 = Random.Range(left, right);
        Vector3 spawnPosition = new Vector3(randomNumber2, 7, 0);
        Asteroid thisAsteroid = Instantiate(AsteroidsList[randomNumber], spawnPosition, Quaternion.identity);
        float fallingSpeed = Random.Range(0.5f, 5f);
        float rotatingSpeed = Random.Range(-10f, 100f);
        thisAsteroid.SetSpeedsStats(new Vector2(fallingSpeed, rotatingSpeed), Random.Range(1, 4));
        asteroids.Add(thisAsteroid.gameObject);
    }


    private void StopsCoroutines()
    {
        PlayerController.instance.StopCoritines();
        StopCoroutine(spawnAsteroidsHere);
        foreach (var item in asteroids)
        {
            //Destroy(item);
            item.GetComponent<Asteroid>().SilentDestroy();
        }
        UIController.Instance.ShowEndScreens(true);

    }

    public void SaveHighScore(int score)
    {
        if (PlayerPrefs.GetInt("maxScore") < score)
        {
            PlayerPrefs.SetInt("maxScore", score);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("maxScore");
    }

}

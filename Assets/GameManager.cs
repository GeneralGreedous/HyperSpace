using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int punkty = 0;
    public bool playing = true;
    [SerializeField] private List<Asteroid> AsteroidsList;
    private Camera mainCamera;
    public Vector2 leftRight;
    public Vector2 downUp;

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
    }

    private void CameraFOV()
    {
        Vector3 leftDown = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 10));
        Vector3 rightUp = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 10));
        leftRight = new Vector2(leftDown.x, rightUp.x);
        downUp = new Vector2(leftDown.y, rightUp.y);
    }
    public void startGame()
    {
        StartCoroutine(SpawnAsteroids());
        PlayerController.instance.startPlayer();
    }
    IEnumerator SpawnAsteroids()
    {
        
        float left = Mathf.Lerp(leftRight.x, leftRight.y, 0.05f);
        float right = Mathf.Lerp(leftRight.x, leftRight.y, 0.95f);
        while (playing)
        {
            int randomNumber = Random.Range(0, AsteroidsList.Count);
            float randomNumber2 = Random.Range(left, right);
            Vector3 spawnPosition = new Vector3(randomNumber2, 7, 0);
            Asteroid thisAsteroid = Instantiate(AsteroidsList[randomNumber], spawnPosition, Quaternion.identity);
            float fallingSpeed = Random.Range(0.5f, 5f);
            float rotatingSpeed = Random.Range(10f, 100f);
            thisAsteroid.SetSpeeds(new Vector2(fallingSpeed, rotatingSpeed));
            yield return new WaitForSeconds(1f);
        }
    }
}

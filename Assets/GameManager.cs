using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int punkty = 0;
    public bool playing = true;
    [SerializeField] private List<Asteroid> AsteroidsList;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (playing)
        {
            int randomNumber = Random.Range(0, AsteroidsList.Count);
            float randomNumber2 = Random.Range(-2.5f, 2.5f);
            Vector3 spawnPosition = new Vector3(randomNumber2, 7, 0);
            Asteroid thisAsteroid = Instantiate(AsteroidsList[randomNumber], spawnPosition, Quaternion.identity);
            float fallingSpeed = Random.Range(0.5f, 5f);
            float rotatingSpeed = Random.Range(10f, 100f);
            thisAsteroid.SetSpeeds(new Vector2(fallingSpeed, rotatingSpeed));
            yield return new WaitForSeconds(1f);
        }
    }
}

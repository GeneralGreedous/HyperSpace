using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private List<Transform> MainGuns;
    [SerializeField] private GameObject StandardBullet;
    public bool shooting = false;
    public bool moving = false;
    float shipHeightPos = 0f;


    private Coroutine MoveShipHere;
    private Coroutine ShootingShipHere;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        Vector2 height = GameManager.Instance.downUp;
        shipHeightPos = Mathf.Lerp(height.x, height.y, 0.1f);
    }

    public void StartPlayer()
    {
        ShootingShipHere = StartCoroutine(Shoot());
        if (MoveShipHere != null)
        {
            StopCoroutine(MoveShipHere);
        }
        MoveShipHere = StartCoroutine(MovingShip());
    }

    public void PreStartPlayer()
    {
        StartCoroutine(FlyToPoint());
    }

    IEnumerator FlyToPoint()
    {
        bool start = false;

        Vector3 startPoint = new Vector3(0, GameManager.Instance.downUp.x - 2f, 0);
        Vector3 endPoint = Vector3.zero;
        endPoint.y = shipHeightPos;
        float time = 0;
        while (!start)
        {

            transform.position = Vector3.Slerp(startPoint, endPoint, time);
            time += Time.deltaTime;
            if (time >= 1f)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }

        GameManager.Instance.StartGame();

    }

    IEnumerator MovingShip()
    {
        moving = true;

        while (moving)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // Get the first touch

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = touch.position;
                    touchPosition.z = 10; // Set a distance from the camera
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                    Debug.Log(worldPosition);
                    worldPosition.y = shipHeightPos;
                    transform.position = worldPosition;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator HideShip()
    {
        bool start = false;

        Vector3 endPoint = new Vector3(0, GameManager.Instance.downUp.x - 2f, 0);
        Vector3 startPoint = transform.position;
        float time = 0;
        while (!start)
        {

            transform.position = Vector3.Slerp(startPoint, endPoint, time);
            time += Time.deltaTime;
            if (time >= 1f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        MoveShipHere = null;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator Shoot()
    {
        shooting = true;
        yield return new WaitForSeconds(.5f);
        while (shooting)
        {
            foreach (Transform trans in MainGuns)
            {
                Instantiate(StandardBullet, trans.position, Quaternion.Euler(0, 0, 90));
            }
            yield return new WaitForSeconds(.3f);
        }
    }

    public void StopCoritines()
    {
        StopCoroutine(MoveShipHere);
        StopCoroutine(ShootingShipHere);
        MoveShipHere = StartCoroutine(HideShip());
    }
}

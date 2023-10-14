using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private List<Transform> MainGuns;
    [SerializeField] private GameObject StandardBullet;
    public bool shooting = true;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(Shoot());
        
    }

    private void Update()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = touch.position;
                touchPosition.z = 10; // Set a distance from the camera
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                Debug.Log(worldPosition);
                worldPosition.y = -4;
                transform.position = worldPosition;
            }
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(.5f);
        while (shooting)
        {
            foreach (Transform trans in MainGuns)
            {
                Instantiate(StandardBullet, trans.position,Quaternion.Euler(0,0,90));
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}

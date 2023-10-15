using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;
    private BoxCollider2D boxCollider;
    [SerializeField] private int points = 1;
    private float _endPoint = -4f;

    private void Awake()
    {
        boxCollider=GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Vector2 downUp = GameManager.Instance.downUp;
        _endPoint = Mathf.Lerp(downUp.x, downUp.y, 0.3f);
    }
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;
        transform.Rotate(rotationSpeed*Time.deltaTime*Vector3.forward);
        if (transform.position.y< _endPoint)
        {
            boxCollider.enabled=false;
            transform.localScale += Vector3.one*Time.deltaTime*-5;
            if (transform.localScale.x<0.1f)
            {
                points = 0;
                Destroy(gameObject);
            }
        }
    }

    public void SetSpeeds(Vector2 speed)
    {
        moveSpeed=speed.x;
        rotationSpeed=speed.y;
    }

    private void OnDestroy()
    {
        GameManager.Instance.punkty += points;
        UIController.Instance.UpdateScore();


        

    }


}

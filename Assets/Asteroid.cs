using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider=GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;
        transform.Rotate(rotationSpeed*Time.deltaTime*Vector3.forward);
        if (transform.position.y<-2f)
        {
            boxCollider.enabled=false;
            transform.localScale += Vector3.one*Time.deltaTime*-5;
            if (transform.localScale.x<0.1f)
            {
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
        Instantiate(explosion, transform.position, Quaternion.identity);
        GameManager.Instance.punkty += 1;
        UIController.Instance.UpdateScore();
    }


}

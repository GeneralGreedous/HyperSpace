using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float life = 1f;
    private Rigidbody2D rb;

    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //transform.position += speed * Time.deltaTime * Vector3.up;
        //rb.transform.position += speed * Time.deltaTime * Vector3.up;
        rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * Vector2.up));
    }
    private void Start()
    {
        Destroy(gameObject, life);
    }

    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}

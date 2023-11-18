using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int points = 1;
    public int Hp
    {
        get; private set;
    }

    [SerializeField] private int maxHp = 2;
    private float _endPoint = -4f;

    [SerializeField] private Color _baseColor = Color.white;
    [SerializeField] private Color _descroyColor = Color.red;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Vector2 downUp = GameManager.Instance.downUp;
        _endPoint = Mathf.Lerp(downUp.x, downUp.y, 0.3f);
        UptadeColor();

    }
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
        if (transform.position.y < _endPoint)
        {
            //boxCollider.enabled=false;
            transform.localScale += Vector3.one * Time.deltaTime * -5;
            if (transform.localScale.x < 0.1f)
            {
                SilentDestroy();
            }
        }
    }
    public void SilentDestroy()
    {
        points = 0;
        Destroy(gameObject);
    }
    public void SetSpeedsStats(Vector2 speed, int hp)
    {
        maxHp = hp;
        Hp = hp;
        moveSpeed = speed.x;
        rotationSpeed = speed.y;
    }

    private void OnDestroy()
    {
        GameManager.Instance.punkty += points;
        GameManager.Instance.RemoveAsterois(this);
        UIController.Instance.UpdateScore();

        if (points > 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

        }
        else
        {
            GameManager.Instance.missedAsteroids += 1;
        }

    }
    private void UptadeColor()
    {

        spriteRenderer.color = Color.Lerp(_baseColor, _descroyColor, 1 - ((float)Hp / (float)maxHp));

    }

    public void getDamege(int damege)
    {
        Hp -= damege;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
        UptadeColor();
    }


}

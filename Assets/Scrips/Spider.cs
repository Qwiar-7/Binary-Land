using UnityEngine;

public class Spider : MonoBehaviour
{
    public float speedMult = 3;
    public GameObject webPrefab;
    Rigidbody2D rigid;
    public int score = 300;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        transform.up = Vector2.left;
        rigid.velocity = Vector2.right * speedMult;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "HeroWeapon")
        {
            GameController.instance.AddScore(score);
            GameController.instance.SpawnEnemyDrop();
            gameObject.SetActive(false);
        }
        else if (tag == "Male" || tag == "Female")
        {
            if (!collision.gameObject.GetComponent<Hero>().isStuck)
            {
                GameObject spiderWeb = Instantiate(webPrefab);
                spiderWeb.transform.position = collision.transform.position;
                GameController.instance.Lose();
            }
        }
        else if (tag == "Wall")
        {
            rigid.velocity *= -1;
            transform.up *= -1;
        }
        else if (tag == "NavPoint")
        {
            int rand = Random.Range(0, 4);
            if (rand == 0)
            {
                transform.up = Vector2.down;
                rigid.velocity = Vector2.up * speedMult;
                transform.position = new Vector2(collision.transform.position.x, transform.position.y);
            }
            else if (rand == 1)
            {
                transform.up = Vector2.left;
                rigid.velocity = Vector2.right * speedMult;
                transform.position = new Vector2(transform.position.x, collision.transform.position.y);
            }
            else if (rand == 2)
            {
                transform.up = Vector2.up;
                rigid.velocity = Vector2.down * speedMult;
                transform.position = new Vector2(collision.transform.position.x, transform.position.y);
            }
            else if (rand == 3)
            {
                transform.up = Vector2.right;
                rigid.velocity = Vector2.left * speedMult;
                transform.position = new Vector2(transform.position.x, collision.transform.position.y);
            }
        }
    }
}

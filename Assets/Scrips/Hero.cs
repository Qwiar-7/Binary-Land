using UnityEngine;

public class Hero : MonoBehaviour
{
    public int speedMult = 5;
    public bool isInverted = false;
    public bool isStuck = false;
    public Animator anim;
    public Vector2[] directions = new Vector2[] { Vector3.right, Vector3.up, Vector3.left, Vector3.down };

    private int direction = -1;
    private Rigidbody2D rigid;
    private GameObject heroWeapon;
    private KeyCode[] keys = new KeyCode[] { KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S };

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        heroWeapon = transform.GetChild(0).gameObject;
        heroWeapon.SetActive(false);
    }
    void Update()
    {
        if (isStuck)
        {
            rigid.velocity = Vector2.zero;
            anim.speed = 0;
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rigid.velocity = Vector2.zero;
            anim.speed = 0;
            heroWeapon.SetActive(true);
        }
        else
        {
            heroWeapon.SetActive(false);
            Move();
        }
    }

    private void Move()
    {
        direction = -1;
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKey(keys[i])) direction = i;
        }

        Vector2 vel = Vector2.zero;
        if (direction > -1)
        {
            vel = directions[direction];
            heroWeapon.transform.localPosition = directions[direction] * 0.7f;
        }
        rigid.velocity = vel * speedMult;

        if (direction == -1)
            anim.speed = 0;
        else
        {
            anim.CrossFade(tag + "_Walk_" + direction, 0);
            anim.speed = 1;
        }
    }
}

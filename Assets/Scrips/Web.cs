using UnityEngine;

public class Web : MonoBehaviour
{
    public Hero catchedHero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "HeroWeapon")
        {
            gameObject.SetActive(false);
        }
        else if (tag == "Male" || tag == "Female")
        {
            if (catchedHero == null)
            {
                collision.gameObject.transform.position = transform.position;
                catchedHero = collision.gameObject.GetComponent<Hero>();
                catchedHero.isStuck = true;
                GameController.instance.CheckWeb();
            }
        }
    }

    private void OnDisable()
    {
        if (catchedHero != null)
        {
            catchedHero.isStuck = false;
            catchedHero.anim.speed = 1;
        }
    }
}

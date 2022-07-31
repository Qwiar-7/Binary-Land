using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public int score = 1000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.instance.AddScore(score);
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Heart : MonoBehaviour
{
    public int triggeredNum = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggeredNum++;
        if (triggeredNum == 2)
        {
            GameController.instance.Win();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggeredNum--;
    }
}

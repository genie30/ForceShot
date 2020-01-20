using UnityEngine;

public class WaterHazard : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.stateRP.Value = GameState.Water;
            }
        }
    }
}

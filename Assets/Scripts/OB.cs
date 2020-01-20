using UnityEngine;

public class OB : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.stateRP.Value = GameState.OB;
        }
    }
}

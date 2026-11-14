using UnityEngine;

public class CoinCountr : MonoBehaviour
{


    void Start()
    {
        CoinPickup[]  c=  GameObject.FindObjectsOfType<CoinPickup>();
        Debug.Log("Coins: " +  c.Length);
    }
}

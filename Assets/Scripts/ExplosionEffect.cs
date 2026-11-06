using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{


    public void Puff(float range)
    {
        transform.localScale = new Vector3(range, range, range);
        Destroy(gameObject, 2);
    }
}

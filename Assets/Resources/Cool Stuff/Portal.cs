
using UnityEngine;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player" )
        {
            other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y + Random.Range(10f, 15f));
            Destroy(this.GetComponent<BoxCollider>());
        }
    }

}

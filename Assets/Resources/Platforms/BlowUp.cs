using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUp : MonoBehaviour
{
    public ParticleSystem explosion;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(BlowThisObject());
        }
    }

    IEnumerator BlowThisObject()
    {
        
        yield return new WaitForSeconds(1f);
        if (explosion != null) explosion.Play();
        yield return new WaitForSeconds(1f);
        Destroy(this.transform.parent.parent.gameObject);
    }
}

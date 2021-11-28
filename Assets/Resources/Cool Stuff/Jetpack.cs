using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{

    public ParticleSystem firstParticle;
    public ParticleSystem secondParticle;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.transform.childCount == 1 ) //only if nothing equipped
        {
            firstParticle.Play();
            secondParticle.Play();

            other.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            this.transform.parent.transform.parent = other.transform;
            this.transform.position = other.transform.position + new Vector3(0f, 0f, 1f);
            this.transform.parent.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().velocity = new Vector3(0f, 20f, 0f);

            this.transform.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(MakeAFly(other.gameObject));

        }
    }

    IEnumerator MakeAFly(GameObject player)
    {
        yield return new WaitForSeconds(5f);

        this.transform.parent.transform.parent = null;
        this.transform.parent.gameObject.AddComponent<Rigidbody>();
        this.transform.parent.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0f, 0f, 40f));

        player.GetComponent<Rigidbody>().useGravity = true;

        firstParticle.Stop();
        secondParticle.Stop();

        yield return new WaitForSeconds(3f);

        Destroy(this.transform.parent.gameObject);
    }
}

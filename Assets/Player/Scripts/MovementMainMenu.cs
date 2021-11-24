
using UnityEngine;

public class MovementMainMenu : MonoBehaviour
{
    [SerializeField]
    private Rigidbody player;

    [SerializeField]
    private Animator animator;
 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" && player.velocity.y <= 1f) player.AddForce(new Vector2(0f, CONSTANTS.JumpForce * 0.7f));

        if (Random.Range(0, 3) == 2)
            if (Random.Range(0, 2) == 1)
                animator.SetTrigger("Flip");
            else
                animator.SetTrigger("Spin");
            
    }
}

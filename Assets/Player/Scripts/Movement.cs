
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody player;


    [SerializeField]
    private Animator animator;

    private bool isPhone = false;
    private bool canMove = false;
    private void Start()
    {
        canMove = true;
        player = this.GetComponent<Rigidbody>();

        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
            isPhone = true;

    }

    private int X = 0; // direction
    void Update()
    {

        if (!isPhone) X = GetDirection();

        if (canMove) player.AddForce(new Vector2(X, 0f) * CONSTANTS.Speed);
        
        CounterMovement(X);

        RotatePlayer(X);

        ClampZCoordinate();

    }

    private int GetDirection()
    {
        if (Input.GetKey(KeyCode.A) ) return -1;
        if (Input.GetKey(KeyCode.D) ) return 1;
        return 0;
    }

    Quaternion desiredRotQR = Quaternion.Euler(0f, 90f, 0f);
    Quaternion desiredRotQL = Quaternion.Euler(0f, -90f, 0f);
    private void RotatePlayer(float X)
    {
        if (X == 0) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 180f, 0f), Time.deltaTime * 2);
        else if (X > 0) transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQR, Time.deltaTime * 2);
        else transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQL, Time.deltaTime * 2);

        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }

    private void CounterMovement(float X)
    {
        if (X == 0 && player.velocity.x != 0)
            player.velocity = Vector3.Lerp(player.velocity, new Vector3(0, player.velocity.y, 0), Time.deltaTime * 3);
        player.velocity = new Vector3(Mathf.Clamp(player.velocity.x, -9, 9), player.velocity.y, 0);
    }

    private void ClampZCoordinate()
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" && player.velocity.y <= 1f ) player.AddForce(new Vector2(0f, CONSTANTS.JumpForce) );
        BatootAndSpringHandle(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "PlatformTrigger") MakeTransparentPlatform(other.transform.parent.GetChild(0).gameObject);

        if (other.tag == "RightEndScreen") player.transform.position = new Vector3(-player.position.x + 3, player.position.y, player.position.z);
        if (other.tag == "LeftEndScreen") player.transform.position = new Vector3(-player.position.x , player.position.y, player.position.z);

        if (other.tag == "Kill") KillEnemy(other.transform.parent.parent.GetComponent<Rigidbody>());

        if (other.tag == "Die" && player.transform.childCount == 1) Die();

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "PlatformTrigger")
        {
            GameObject platform = other.transform.parent.GetChild(0).gameObject;
            Physics.IgnoreCollision(platform.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            platform.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1f);
        }

    }

    private void KillEnemy(Rigidbody enemy)
    {
        enemy.transform.GetChild(0).GetChild(1).GetComponent<Collider>().enabled = false;
        player.AddForce(new Vector2(0f, CONSTANTS.JumpForce));
    
        enemy.isKinematic = false;
        enemy.AddForce(new Vector2(0f, CONSTANTS.EnemyHitForce));
    }
    

    private void BatootAndSpringHandle(Collision collision)
    {
        if (collision.gameObject.tag == "Batoot")
        {
            player.AddForce(new Vector2(0f, CONSTANTS.BatootForce));
            animator.SetTrigger("Flip");

        }
        if (collision.gameObject.tag == "Spring")
        {
            player.AddForce(new Vector2(0f, CONSTANTS.SpringForce));
            animator.SetTrigger("Spin");
        }
            
    }

    private void MakeTransparentPlatform(GameObject platform)
    {
        Physics.IgnoreCollision(platform.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        platform.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.2f);
    }

    private void Die()
    {
        canMove = false;
        Physics.IgnoreLayerCollision(6, 0);
        player.AddForce(new Vector2(0f, CONSTANTS.EnemyHitForce));
        animator.SetTrigger("Die");
    }

    public void GoRight()
    {
        X = 1;
    }

    public void GoLeft()
    {
        X = -1;
    }

    public void StopMoving()
    {
        X = 0;
    }

}

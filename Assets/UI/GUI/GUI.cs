using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class GUI : MonoBehaviour
{
    private Transform player;
    private TMP_Text currentPoints;

    [SerializeField]
    private GameObject phoneControls;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentPoints = GameObject.FindGameObjectWithTag("CurrentPoints").GetComponent<TMP_Text>();

        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
            phoneControls.SetActive(true);
    }
    public void Restart()
    {
        player.position = Vector3.zero;
    }

    private void FixedUpdate()
    {
        currentPoints.text = Mathf.Max ( ((int)player.position.y), int.Parse(currentPoints.text) ).ToString();
    }
}

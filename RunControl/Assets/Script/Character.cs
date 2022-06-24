using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public MainControl mainControl;
    public Camera MyCam;
     bool isStartFight;
    public GameObject MidArea;
    private void FixedUpdate()
    {
        if(!isStartFight)
        transform.Translate(Vector3.forward * .5f * Time.deltaTime);
    }
    void Update()
    {
        if (!isStartFight)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
                }
                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                }
            }
        }
        else       
           transform.position = Vector3.Lerp(transform.position, MidArea.transform.position, .007f);         
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Divided")|| other.CompareTag("Multiply")|| other.CompareTag("Minus")|| other.CompareTag("Plus"))
        {
            mainControl.NpcCharacterManager(other.gameObject.tag, int.Parse(other.name) ,other.transform);
        }
        if (other.CompareTag("FinalCollider"))
        {
            mainControl.StartFinalFight();
            MyCam.GetComponent<CameraFollow>().isStartFightCam = true;
            isStartFight = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Column")|| collision.gameObject.CompareTag("NeedleBox"))
        {
            if (gameObject.transform.position.x<0)
                transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
        }
    }
}

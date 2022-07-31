using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    Camera cam;
    bool IsCollider = true;
    bool isStartFight;
    [Header("General Operation")]
    [SerializeField]  float SwipeSpeed;
    [SerializeField] GameObject MidArea;
    [SerializeField] Slider slider;
    [SerializeField] GameObject TargetDsitance;
    [Header("Cam Operation")]
    [SerializeField] MainControl mainControl;
    void FixedUpdate()
    {
        if(!isStartFight)
        transform.Translate(Vector3.forward * .5f * Time.deltaTime);
    }
    void Start()
    {
        cam = Camera.main;
        slider.maxValue = Vector3.Distance(transform.position, TargetDsitance.transform.position);
    }
    void Update()
    {
      if (Time.timeScale!=0)
      {     
        if (!isStartFight)
        {
            slider.value = Vector3.Distance(transform.position, TargetDsitance.transform.position);          
                if (Input.GetButton("Fire1"))
                {
                    Move();
                }
            }
        else
        {
            slider.value -= .005f; 
            transform.position = Vector3.Lerp(transform.position, MidArea.transform.position, .007f);
        }
      }
    }

    private void Move()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.transform.localPosition.z;
        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {       
            Vector3 hitVec = hit.point;
            hitVec.y = gameObject.transform.localPosition.y;
            hitVec.z = gameObject.transform.localPosition.z;
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, hitVec, Time.deltaTime * SwipeSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsCollider)
        {
            if (other.CompareTag("Divided") || other.CompareTag("Multiply") || other.CompareTag("Minus") || other.CompareTag("Plus"))
            {
                IsCollider = false;
                mainControl.NpcCharacterManager(other.gameObject.tag, int.Parse(other.name), other.transform);
                StartCoroutine(TriggerControl());
            }
            if (other.CompareTag("FinalCollider"))
            {
                IsCollider = false;
                mainControl.StartFinalFight();
                cam.GetComponent<CameraFollow>().isStartFightCam = true;
                isStartFight = true;
            }
        }
      
    }
    IEnumerator TriggerControl()
    {
        yield return new WaitForSeconds(0.43f);
        IsCollider = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Column") || collision.gameObject.CompareTag("NeedleBox"))
        {      
                if (gameObject.transform.position.x < 0)
                    transform.position = new Vector3(transform.position.x + 0.13f, transform.position.y, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x - 0.13f, transform.position.y, transform.position.z);
               
        }
    }
}

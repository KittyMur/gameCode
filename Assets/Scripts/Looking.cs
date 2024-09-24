using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Looking : MonoBehaviour
{
    public float mouseSens;
    public Transform player;
    float xRot = 0f;
    public bool nrot = true;
    //public TMP_Text mouse;
    //public Slider slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //mouse.text = slider.value.ToString();
        //mouseSens = slider.value;

        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -30f, 30f);

        if (nrot == true)
        {
            transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);
        }
        else
        {
            mouseSens = 0f;
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            player.Rotate(Vector3.up * 0);
        }
    }
}

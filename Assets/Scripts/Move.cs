using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [Range(100f, 500f)]
    public float runSpeed = 500f;

    public float jumpforse = 0f;

    private float mSpeed;

    Rigidbody rb;

    public Camera cam;
    public GameObject cam0;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;

    float baseFOV;
    public float sprintFOV = 1.25f;

    Animator anim;

    public GameObject e;
    public GameObject tab;
    public GameObject space;
    public GameObject Rock;
    public GameObject boom;
    public bool boombala = false;

    public GameObject stars;
    public float time;

    public GameObject colob;
    public Object dialogue_colob;
    public Object dialogue_colob2;
    public Object dialogue_fish;
    public Object dialogue_fish2;
    public Object dialogue_gek;

    public GameObject hummer;
    public GameObject hummer1;
    public bool hum = false;

    public LayerMask ground;
    public Transform groundDetector;

    public GameObject humster;
    public GameObject humsterDist;
    public GameObject humster1;
    public GameObject puf;

    public bool musk = false;

    public GameObject menu;
    public Text esc;
    public GameObject escs;

    public TMP_Text mouse;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        escs.SetActive(true);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        e.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        colob.GetComponent<ColobScript>().dialogue = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouse.text = slider.value.ToString();
        cam.GetComponent<Looking>().mouseSens = slider.value;

        if (Input.GetKey(KeyCode.Q))
        {
            menu.SetActive(true);
            esc.color = Color.gray;
            FindObjectOfType<AudioManager>().Play("E");

            if (Cursor.lockState != CursorLockMode.Confined)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                cam.GetComponent<Looking>().nrot = false;
            }
        }

        if (cam.GetComponent<Looking>().nrot == true)
        {
            escs.SetActive(true);

            var distanses = (e.transform.position - rb.transform.position).magnitude;
            if (distanses >= 20f) colob.GetComponent<ColobScript>().dialogue = null;

            time -= Time.deltaTime;
            if (time <= 0f)
            {
                stars.SetActive(false);
                time = 0f;
            }

            e.transform.LookAt(cam.transform);
            tab.transform.LookAt(cam.transform);
            if (space != null) space.transform.LookAt(cam.transform);
            bool groundcCheck = Physics.Raycast(groundDetector.position, Vector3.down, 0.5f, ground);
            bool jump = Input.GetKey(KeyCode.Space) && groundcCheck;

            if (jump == true && jumpforse > 0f)
            {
                rb.AddForce(Vector3.up * jumpforse);
                FindObjectOfType<AudioManager>().Play("Jump"); //звук прыжка
            }

            float xMove = Input.GetAxisRaw("Horizontal");
            float zMove = Input.GetAxisRaw("Vertical");


            if (zMove != 0)
            {
                mSpeed = runSpeed;
                anim.SetInteger("state", 2);
                //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, baseFOV * sprintFOV, Time.fixedDeltaTime * 8f);
            }
            else
            {
                anim.SetInteger("state", 3);
                //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, baseFOV, Time.fixedDeltaTime * 8f);
            }

            Vector3 dir = new Vector3(xMove, 0, zMove);
            dir.Normalize();

            Vector3 v = transform.TransformDirection(dir) * mSpeed * Time.fixedDeltaTime;
            v.y = rb.velocity.y;
            rb.velocity = v;
        }

        if (Input.GetKey(KeyCode.E))
        {            
            var distanses = (e.transform.position - rb.transform.position).magnitude;
            if (distanses <= 20f) 
            {
                escs.SetActive(false);
                FindObjectOfType<AudioManager>().Play("E");

                colob.GetComponent<ColobScript>().interact();
                e.SetActive(false);
                cam0.SetActive(true);

                if (Cursor.lockState != CursorLockMode.Confined)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    cam.GetComponent<Looking>().nrot = false;
                }

                if (hum == true)
                {
                    hummer1.SetActive(true);
                    hummer.SetActive(false);
                }
            }
            else
            {
                escs.SetActive(false);
                colob.GetComponent<ColobScript>().dialogue = null;
                e.SetActive(false);
                cam1.SetActive(false);
                cam2.SetActive(false);
                cam3.SetActive(false);
            }
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            var distanses = (tab.transform.position - rb.transform.position).magnitude;
            if (distanses <= 20f)
            {
                escs.SetActive(false);
                FindObjectOfType<AudioManager>().Play("E");

                var distanse = (humsterDist.transform.position - rb.transform.position).magnitude;
                if (distanse <= 10f) HumHum();
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            space.SetActive(false);

            var distanse = (Rock.transform.position - rb.transform.position).magnitude;
            if (hummer1.active == true && distanse <= 10f)
            {
                Rock.SetActive(false);
                space.SetActive(false);
                hummer1.SetActive(false);
                boom.SetActive(true);
                hum = false;
                boombala = true;
                FindObjectOfType<AudioManager>().Play("boom");
            }
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("trig"))
        {
            cam0 = cam1;
            e.transform.position = new Vector3(36.31f, 10.42f, 90.57f);
            e.SetActive(true);
            colob.GetComponent<ColobScript>().dialogue = dialogue_colob;
            if (musk == true)
            {
                colob.GetComponent<ColobScript>().dialogue = dialogue_colob2;
            }
        }

        if (other.gameObject.CompareTag("trigFish"))
        {
            cam0 = cam2;
            e.transform.position = new Vector3(-16.25f, 4.51f, 83.85f);
            e.SetActive(true);
            colob.GetComponent<ColobScript>().dialogue = dialogue_fish;
            if (boombala == true) colob.GetComponent<ColobScript>().dialogue = dialogue_fish2;
        }

        if (other.gameObject.CompareTag("trigGek"))
        {
            cam0 = cam3;
            e.transform.position = new Vector3(-76.68f, 8.20f, 88.24f);
            e.SetActive(true);
            colob.GetComponent<ColobScript>().dialogue = dialogue_gek;
            hum = true;
        }

        if (other.gameObject.CompareTag("trigrock"))
        {
            space.SetActive(true);
            e.SetActive(false);
        }

        if (other.gameObject.CompareTag("trighum"))
        {
            tab.transform.position = new Vector3(25.69f, 6.43f, -75.75f);
            tab.SetActive(true);
        }
    }
    public void damage()
    {
        anim.SetInteger("state", 4);
        FindObjectOfType<AudioManager>().Play("bird");
        stars.SetActive(true);
        time = 1f;
    }

    public void HumHum()
    {
        tab.SetActive(false);
        humster.SetActive(false);
        humster1.SetActive(true);
        puf.SetActive(true);
        FindObjectOfType<AudioManager>().Play("puf");
        musk = true;
    }
}

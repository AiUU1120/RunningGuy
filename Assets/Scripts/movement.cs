using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpforce;
    public AudioSource jumpAudio, hurtAudio, collectAudio, deathAudio;
    public Collider2D coll;
    public Animator anim;
    public LayerMask ground;
    public int cherry;
    public Text CherryNum;
    public int health;
    public Text HealthNum;
    bool action = false;
    private bool IsHurt;
    public GameObject deadLine;
    public Joystick joystick;
    public bool tempflag = false;
    private float tem = 0f;


    void Start()
    {
        
    }

    private void Update()
    {
        Jump();

        if(Input.GetKey(KeyCode.K))
        {
            tempflag = true;
        }

        if(tempflag)
        {
            transform.Rotate(Vector3.up, 0.1f);
            //tem -= 0.1f;
            if(transform.rotation.y >= 90f)
            {
                //transform.localRotation = Quaternion.Euler(0f, tem, 0f);
                Debug.Log("OK");
                tempflag = false;
            }
            Debug.Log("test");
        }
    }
    
    void FixedUpdate()
    {
        if (!IsHurt)
        {
            Movement();
        }
        Swithanim();
    }

    void Movement()//��ɫ�ƶ�
    {
        float horizontalmove = Input.GetAxis("Horizontal");//PC����
        //float horizontalmove = joystick.Horizontal;//Mobile����
        float facedirection = Input.GetAxisRaw("Horizontal");//PC����
        //float facedirection = joystick.Horizontal;//Mobile����
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2 (horizontalmove * speed * Time.fixedDeltaTime , rb.velocity.y);
            anim.SetFloat ("running", Mathf.Abs (facedirection));
        }

        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection,1,1);
        }
        /*if (facedirection > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (facedirection < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }*///Mobile����

    }

    void Jump()//��ɫ��Ծ
    {
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))//PC����
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
        /*if (joystick.Vertical > 0.5f && coll.IsTouchingLayers(ground))//Mobile����
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }*/
    }

    void Swithanim()//�л���ɫ����
    {
        //anim.SetBool("idle", false);

        if(rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping",false); 
                anim.SetBool("falling",true);
            }
        }
        else if(IsHurt)
        {
            anim.SetFloat("running", 0);
            if(Mathf.Abs(rb.velocity.x) < 3f)
            {
                anim.SetBool("hurt", false);
                //anim.SetBool("idle", true);
                IsHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            //anim.SetBool("idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//��ײ������
    {
        if(collision.tag == "Object")//��ײӣ��
        {
            Destroy(collision.gameObject);
            cherry += 1;
            collision.tag = "Null";
            collectAudio.Play();
            CherryNum.text = cherry.ToString();
        }
        if (collision.tag == "Deadline")
        {
            GetComponent<AudioSource>().enabled = false;
            rb.velocity = Vector3.zero;
            deathAudio.Play();
            Invoke("Restart", 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//��������
    {
        if (collision.gameObject.tag == "Enemies")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling")&&transform.position.y>(collision.gameObject.transform.position.y)+1)//�������
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)//����
            {
                rb.velocity=new Vector2(-10,rb.velocity.y);
                IsHurt = true;
                health -= 1;
                hurtAudio.Play();
                HealthNum.text = health.ToString();
                anim.SetBool("hurt", true);
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                IsHurt = true;
                health -= 1;
                hurtAudio.Play();
                HealthNum.text = health.ToString();
                anim.SetBool("hurt", true);
            }
            Dead();
        }
        if (collision.gameObject.tag == "Wifus")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling") && transform.position.y > (collision.gameObject.transform.position.y) + 1)//�������
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
        }
    }

    void Restart()//��������
    {
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Dead()
    {
        if(health <= 0)
        {
            deadLine.SetActive(true);
            GetComponent<AudioSource>().enabled = false;
            rb.velocity = Vector3.zero;
            deathAudio.Play();
            Invoke("Restart", 1f);
        }
    }
}

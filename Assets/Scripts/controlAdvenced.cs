using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlAdvenced : MonoBehaviour
{
    public static controlAdvenced instance;
    private Rigidbody2D rb;
    public float speed = 5f;
    private float speedSave;

    public float jump = 8f;
    public int nombreDeSaut = 1;
    public bool wallJumping;
    private int nombreDeSautSave;
    public float longueurCheckJump = 1.1f;
    public LayerMask monLayer;

    private Vector2 mouvement;
    private bool canJump;
    private bool jeMeTouche;
    private SpriteRenderer skin;
    public Animator animatotor;
    private Collider2D hit;
    private RaycastHit2D hit2;
    private CircleCollider2D monCollider;
    private bool wallJump;

    // Use this for initialization
    void Start() {
        if(instance == null)
        {
            instance = this;
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        skin = gameObject.GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;
        monCollider = gameObject.GetComponent<CircleCollider2D>();
        animatotor = gameObject.GetComponent<Animator>();
        speedSave = speed;
        nombreDeSautSave = nombreDeSaut;
    }
    // Update is called once per frame
    void Update() {
        if (canJump && jeMeTouche) {
            nombreDeSaut = nombreDeSautSave;
        }

        if (!wallJump) {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && nombreDeSaut > 0) {
            rb.velocity = new Vector2(rb.velocity.x, jump);            
            nombreDeSaut--;
            animatotor.SetTrigger("jump");
        }

        jumpCheck();
        if (!canJump && wallJumping) {
            wallCheck();
        }
        
        if (skin != null) {
            skinCheck();
        }

        animatotor.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animatotor.SetFloat("velocityY", rb.velocity.y);
    }

    void jumpCheck() {
        hit = Physics2D.OverlapCircle(transform.position - new Vector3(0, longueurCheckJump, 0), monCollider.radius * 0.9f * transform.localScale.x, monLayer);
        if (hit && !hit.isTrigger) {
            canJump = true;
            animatotor.SetBool("jumpBool", false);
        } else {
            canJump = false;
            animatotor.SetBool("jumpBool", true);
        }
    }

    void wallCheck () {
        hit2 = Physics2D.Raycast(transform.position, Vector2.right, (monCollider.bounds.extents.x + monCollider.offset.x) * 1.1f);
        Debug.DrawRay(transform.position, Vector2.right * monCollider.bounds.extents.x * 1.1f, Color.red);        

        if (hit2.collider != null) {
            if (Input.GetButtonDown("Jump")) {
                wallJump = true;
                StartCoroutine("wallJumpingRoutine");
                rb.velocity = new Vector2(-jump, jump);
            }
        }

        hit2 = Physics2D.Raycast(transform.position, Vector2.left, monCollider.bounds.extents.x * 1.1f);
        Debug.DrawRay(transform.position, Vector2.left * monCollider.bounds.extents.x * 1.1f, Color.red);

        if (hit2.collider != null) {
            if (Input.GetButtonDown("Jump")) {
                wallJump = true;
                StartCoroutine("wallJumpingRoutine");
                rb.velocity = new Vector2(jump, jump);
            }
        }
    }

    void skinCheck() {
        if (Input.GetAxisRaw("Horizontal") > 0) {
            skin.flipX = false;
        }
        if (Input.GetAxisRaw("Horizontal") < 0) {
            skin.flipX = true;
        }
    }

    void OnCollisionStay2D(Collision2D trucQuiMeTouche) {
        jeMeTouche = true;
    }

    void OnCollisionExit2D(Collision2D trucQuiMeTouche) {
        jeMeTouche = false;
        nombreDeSaut--;
    }

    IEnumerator wallJumpingRoutine () {
        yield return new WaitForSeconds(0.4f);
        wallJump = false;
    }

    void OnDrawGizmosSelected() {
        if (monCollider == null) {
            monCollider = gameObject.GetComponent<CircleCollider2D>();
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, longueurCheckJump, 0), monCollider.radius * 0.9f * transform.localScale.x);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocon : MonoBehaviour
{

    public float moveSpeed;     //G�re la vitesse de d�placement
    public float jumpforce;     //G�re la puissance de saut
    float input;

    public bool isJumping;         //Check si le personnage est en l'air
    public bool isGrounded;        //Check si le personnage est au sol

    public Transform groundCheck;       //Permet de d�finir la position du Checker
    public float groundCheckRadius;     //Permet de d�finir le radius autour du checker
    public LayerMask collisionLayers;   //Permet de choisir si l'on souhaite des collisions entre certains layer ou non

    public Rigidbody2D rb;                      //Associe le personnage � une variable
    public Animator animator;                   //Permet de d�finir l'animator g�rant les animations
    public CircleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;    //Vecteur permettant le mouvement du joueur
    private float horizontalMovement;           //Permet de d�finir le mouvement horizontal

    public static Flocon instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Flocon dans la sc�ne");
            return;
        }

        instance = this;
    }
    private void Update()
    {
        //Permet de checker si le personnage est au sol ou non
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers); //On cr�� donc un cercle partant de la position du checker, 
        //avec un radius d�finie et on d�finie avec quel layer nous ne souhaitons pas avoir de collision � savoir ici le layer Player



        //Pour l'animation des mouvements
        Flip(rb.velocity.x);                                    //On appelle la fonction flip pour connaitre la direction du personnage et le tourner en fonction

        float characterVelocity = Mathf.Abs(rb.velocity.x);     //Permet de renvoyer une valeur toujours positive, ici notre vitesse
        animator.SetFloat("Speed", characterVelocity);          //On envoie � l'animator la valeur de la vitesse du personnage sur x et il va pouvoir savoir quel animation jouer


        //Permet de savoir si le personnage peut sauter ou non
        if (Input.GetKeyDown(KeyCode.Space))      //Si la touche espace est appuyez
        {
            Debug.Log("Saut");
            if (isGrounded == true)                 //Si le personnage est au sol
            {
                Debug.Log("Saut2");
                isJumping = true;                   //Alors on dit que le personnage peut sauter
            }

        }

    }

    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");
        horizontalMovement = input * moveSpeed * Time.deltaTime;        //Permet de d�finir le mouvement horizontal et sa puissance en fonction de la touche pr�ss�, la vitesse et le temps

        //Active la fonctionne permettant au personnage de bouger (saut + mouvement horizontaux)
        MovePlayer(horizontalMovement);                         //On envoie � la fonction r�alisant le mouvement du joueur la puissance et la direction du mouvement

    }



    void MovePlayer(float _horizontalMovement)                                              //Fonction r�alisant le mouvement du joueur
    {

        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);           //Cr�� un vecteur d�sigant la direction dans laquel va aller le personnage. Le x correspond � la vitesse calculer et le y c est la vitesse du personnage � ce moment la
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);  //Permet au personnage de se d�placer de fa�on progressive allant de sa position au vecteur d�clarer au dessus

        if (isJumping == true)                                                               //Si le personnage est sur le point de sauter
        {
            Debug.Log("Saut3");
            rb.velocity = Vector2.up * jumpforce;                                           //On donne une force sur l'axe y au personnage qui correspond � la force d�finir au d�but
            isJumping = false;                                                              //Puis on dit que le saut � �t� r�alis�
            animator.SetTrigger("jump");
        }
    }


    void Flip(float _velocity)                  //Permet de retourner le personnage lors de l'animation en fonction de sa position
    {
        if (_velocity > 0.1f)                   //Si le personnage va � droite
        {
            transform.eulerAngles = new Vector3(0, 0, 0);       //Alors il ne flip pas son animation
        }
        else if (_velocity < -0.1f)             //Par contre si le personnage va � gauche
        {
            transform.eulerAngles = new Vector3(0, -180, 0);       //Alors il flip son animation
        }
    }


    private void OnDrawGizmos()                                         //On cr�� cette fonction afin de voir le radius autour de notre checker
    {
        Gizmos.color = Color.red;                                       //On lui donne une couleur rouge
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); //On d�finie un cercle qui � pour centre le checker et comme radius le radius du checker
    }
}

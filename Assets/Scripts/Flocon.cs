using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocon : MonoBehaviour
{

    public float moveSpeed;     //Gère la vitesse de déplacement
    public float jumpforce;     //Gère la puissance de saut
    float input;

    public bool isJumping;         //Check si le personnage est en l'air
    public bool isGrounded;        //Check si le personnage est au sol

    public Transform groundCheck;       //Permet de définir la position du Checker
    public float groundCheckRadius;     //Permet de définir le radius autour du checker
    public LayerMask collisionLayers;   //Permet de choisir si l'on souhaite des collisions entre certains layer ou non

    public Rigidbody2D rb;                      //Associe le personnage à une variable
    public Animator animator;                   //Permet de définir l'animator gérant les animations
    public CircleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;    //Vecteur permettant le mouvement du joueur
    private float horizontalMovement;           //Permet de définir le mouvement horizontal

    public static Flocon instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Flocon dans la scène");
            return;
        }

        instance = this;
    }
    private void Update()
    {
        //Permet de checker si le personnage est au sol ou non
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers); //On créé donc un cercle partant de la position du checker, 
        //avec un radius définie et on définie avec quel layer nous ne souhaitons pas avoir de collision à savoir ici le layer Player



        //Pour l'animation des mouvements
        Flip(rb.velocity.x);                                    //On appelle la fonction flip pour connaitre la direction du personnage et le tourner en fonction

        float characterVelocity = Mathf.Abs(rb.velocity.x);     //Permet de renvoyer une valeur toujours positive, ici notre vitesse
        animator.SetFloat("Speed", characterVelocity);          //On envoie à l'animator la valeur de la vitesse du personnage sur x et il va pouvoir savoir quel animation jouer


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
        horizontalMovement = input * moveSpeed * Time.deltaTime;        //Permet de définir le mouvement horizontal et sa puissance en fonction de la touche préssé, la vitesse et le temps

        //Active la fonctionne permettant au personnage de bouger (saut + mouvement horizontaux)
        MovePlayer(horizontalMovement);                         //On envoie à la fonction réalisant le mouvement du joueur la puissance et la direction du mouvement

    }



    void MovePlayer(float _horizontalMovement)                                              //Fonction réalisant le mouvement du joueur
    {

        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);           //Créé un vecteur désigant la direction dans laquel va aller le personnage. Le x correspond à la vitesse calculer et le y c est la vitesse du personnage à ce moment la
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);  //Permet au personnage de se déplacer de façon progressive allant de sa position au vecteur déclarer au dessus

        if (isJumping == true)                                                               //Si le personnage est sur le point de sauter
        {
            Debug.Log("Saut3");
            rb.velocity = Vector2.up * jumpforce;                                           //On donne une force sur l'axe y au personnage qui correspond à la force définir au début
            isJumping = false;                                                              //Puis on dit que le saut à été réalisé
            animator.SetTrigger("jump");
        }
    }


    void Flip(float _velocity)                  //Permet de retourner le personnage lors de l'animation en fonction de sa position
    {
        if (_velocity > 0.1f)                   //Si le personnage va à droite
        {
            transform.eulerAngles = new Vector3(0, 0, 0);       //Alors il ne flip pas son animation
        }
        else if (_velocity < -0.1f)             //Par contre si le personnage va à gauche
        {
            transform.eulerAngles = new Vector3(0, -180, 0);       //Alors il flip son animation
        }
    }


    private void OnDrawGizmos()                                         //On créé cette fonction afin de voir le radius autour de notre checker
    {
        Gizmos.color = Color.red;                                       //On lui donne une couleur rouge
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); //On définie un cercle qui à pour centre le checker et comme radius le radius du checker
    }
}

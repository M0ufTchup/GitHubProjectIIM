using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;       //ici le gameObject est le joueur
    public float timeOffset;        //la caméra obtient un décalage de temps
    public Vector3 posOffset;       //c'est ce qui nous permet d'avoir la vitesse de déplacement de la caméra

    private Vector3 velocity;       //Réfèrence pour le SmoothDamp, le SmoothDamp permet de faire déplacer la caméra à un endroit ou un personnage donné et elle le suivra ensuite

    void Update()
    {
        //Permet d'accéder à la position de la caméra et de déplacer l'objet d'un endroit à un autre à une certaine vitesse
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
                                    
        //On définie d'abord la position ou se trouve la caméra, puis l'endroit où elle va se déplacer à savoir la position du joueur avec un décalage si besoin,
        //on va définir à partir de notre référence de base le temps que va mettre la caméra pour se déplacer.
    }
}

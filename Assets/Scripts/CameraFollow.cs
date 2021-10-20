using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;       //ici le gameObject est le joueur
    public float timeOffset;        //la cam�ra obtient un d�calage de temps
    public Vector3 posOffset;       //c'est ce qui nous permet d'avoir la vitesse de d�placement de la cam�ra

    private Vector3 velocity;       //R�f�rence pour le SmoothDamp, le SmoothDamp permet de faire d�placer la cam�ra � un endroit ou un personnage donn� et elle le suivra ensuite

    void Update()
    {
        //Permet d'acc�der � la position de la cam�ra et de d�placer l'objet d'un endroit � un autre � une certaine vitesse
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
                                    
        //On d�finie d'abord la position ou se trouve la cam�ra, puis l'endroit o� elle va se d�placer � savoir la position du joueur avec un d�calage si besoin,
        //on va d�finir � partir de notre r�f�rence de base le temps que va mettre la cam�ra pour se d�placer.
    }
}

using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_movement : MonoBehaviour
{


    [SerializeField] float jumpForce = 8;
    [SerializeField] float gravity = 20;

    public float sideSpeed = 5;
    Vector3 moveInput;
    public GameManager_Script gameManager;
    public float posMin = -4.045702f;
    public float posMax = 3.79f;
    CharacterController charController;
    Vector3 jumpValue = new Vector3(0, 0, 0);

    //FX
    [SerializeField] GameObject explosion;

    //Audio
    AudioSource audio;

    //UI
    public TextMeshProUGUI score;
    int coins;


    void Start()
    {
        //Obtenir les composants nécessaires
        charController = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        //Calculer le mouvement latéral
        Vector3 move = transform.right * moveInput.x * sideSpeed;

        //Appliquer la gravité
        if (!charController.isGrounded)
        {
            jumpValue -= transform.up * Time.deltaTime * gravity;
        }
        //Limiter la vitesse de chute
        else
        {
            jumpValue.y = Mathf.Max(-1, jumpValue.y);
        }
        //Appliquer le mouvement
        charController.Move((move + jumpValue) * Time.deltaTime);

        //Limiter la position latérale
        Vector3 pos = transform.position;
        pos.z = Mathf.Clamp(pos.z, posMin, posMax);
        transform.position = pos;
    }

    //C'est grace à lui qu'on détecte le mouvement avec le input player et c'est lui qui nous donne les moveInput.x .z
    public void InputMove(InputAction.CallbackContext move)
    {
        Vector2 moveTemp = move.ReadValue<Vector2>();
        moveInput = new Vector3(moveTemp.x, 0, 0);
    }

    //C'est grace à lui qu'on détecte si le joueur appuie sur le bouton de saut et c'est lui qui nous donne le jumpValue.y
    public void InputJump(InputAction.CallbackContext context)
    {
        if (charController.isGrounded)
        {
            jumpValue = transform.up * jumpForce;
        }     
    }

    private void OnTriggerEnter(Collider other)
    {
        //Détecter les collisions avec les triggers, les pièces et les barrières

        //Si le joueur entre dans un trigger nommé "Trigger", créer une nouvelle map
        if (other.gameObject.CompareTag("Trigger"))
        {
            gameManager.CreateMap();
        }

        //Si le joueur entre en collision avec un objet tagué "coins", jouer un son, augmenter le nombre de pièces, mettre à jour le score et détruire la pièce
        //et augmenter la vitesse du jeu avec la fonction TakeCoin() du GameManager
        else if (other.gameObject.CompareTag("coins"))
        {
            audio.Play();
            coins++;
            score.text = coins.ToString() + " Coins";
            Destroy(other.gameObject);
            gameManager.TakeCoin();
        }
        //Si le joueur entre en collision avec un objet tagué "barrieres", arrêter le jeu avec la fonction Stop() du GameManager,
        //instancier une explosion et détruire le joueur
        else if (other.gameObject.CompareTag("barrieres"))
        {
            gameManager.Stop();
            Instantiate(explosion, transform.position + Vector3.up, Quaternion.identity);
            Destroy(gameObject);
        }
    }






}

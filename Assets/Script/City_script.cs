using UnityEngine;

public class City_script : MonoBehaviour
{
    [SerializeField] float destroyDistance = -60.84f;
    public GameManager_Script gameManage;

    void Start()
    {
        //Obtenir une référence au GameManager_Script
        gameManage = FindObjectOfType<GameManager_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        //Déplacer la ville vers la gauche en fonction de la vitesse du jeu
        transform.position += new Vector3(-2, 0, 0)*gameManage.speed*Time.deltaTime;
        if (transform.position.x<=destroyDistance)
        {
            Destroy(gameObject);
        }
    }

   




}

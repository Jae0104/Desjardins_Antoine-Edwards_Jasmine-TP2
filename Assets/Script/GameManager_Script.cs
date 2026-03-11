using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameManager_Script : MonoBehaviour
{
    public GameObject barriere;
    public GameObject roadSection;
    public GameObject coins;
    public float barriereMin = -4.045702f;
    public float barriereMax = 1.42f;
    public float coinMin = -4.045702f;
    public float coinMax = 3.79f;
    public float speed=1f;

    void Start()
    {
        //Appeler la fonction pour faire apparaître les barrières et les pièces au début du jeu
        PopObject(GameObject.FindGameObjectWithTag("city"));
    }

    void Update()
    {
        
    }

    public void PopObject(GameObject city)
    {
        //Faire apparaître les barrières et les pièces à des positions aléatoires dans la ville
        float distanceBarriere = -4.046037f;
        float distanceCoin=-7.31f;
        for (int i = 0; i <= 5; i++)
        {
            float barriereSpawn = Random.Range(barriereMin, barriereMax);
            float coinSpawn = Random.Range(coinMin,coinMax);
            Instantiate(barriere,new Vector3(city.transform.position.x+distanceBarriere,0,barriereSpawn),Quaternion.identity,city.transform);
            Instantiate(coins, new Vector3(city.transform.position.x + distanceCoin, 0.5f, coinSpawn), coins.transform.rotation, city.transform);
            distanceBarriere += 9;
            distanceCoin += 9;
        }
    }

    public void CreateMap()
    {
        //Créer une nouvelle section de route à la fin de la ville actuelle et faire apparaître les barrières et les pièces dans cette nouvelle section
        GameObject newCity = Instantiate(roadSection, new Vector3(36f, 0, 0), Quaternion.identity);
        PopObject(newCity);
        
    }

    //Augmenter la vitesse du jeu à chaque fois que le joueur ramasse une pièce
    public void TakeCoin()
    {
        speed += 1;
    }

    //Arrêter le jeu en mettant la vitesse à 0
    public void Stop()
    {
        speed = 0;
    }

    
}

using UnityEngine;

public class EnemyManagers : MonoBehaviour
{
    public PlayerHeal playerHeal;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    //[SerializeField]
    //MonoBehaviour factory;
    //IFactory Factory { get { return factory as IFactory; } }

    void Start()
    {
        // Mengeksekusi fungsi spawn setiap beberapa detik sesuai dengan nilai spawnTime
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        // Jika player telah mati maka tidak membuat enemy baru 
        if (playerHeal.currentHealth <= 0f) return;
        
        // Mendapatkan nilai random
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        //int spawnEnemy = Random.Range(0, spawnPoints.Length);

        // Menduplikasi enemy
        //Factory.FactoryMethod(spawnEnemy);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }    
}

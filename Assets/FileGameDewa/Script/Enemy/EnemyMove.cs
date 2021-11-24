using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Transform player;
    PlayerHeal playerHeal;
    EnemyHeal enemyHeal;
    NavMeshAgent nav;

    void Awake()
    {
        // cari game object dengan tag player
        player = GameObject.FindGameObjectWithTag("PlayerShoot").transform;

        // mendapatkan reference component
        playerHeal = player.GetComponent<PlayerHeal>();
        enemyHeal = GetComponent<EnemyHeal>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // memindahkan posisi player
        if(enemyHeal.currentHealth > 0 && playerHeal.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else // hentikan moving
        {
           nav.enabled = false;
        }
    }
}

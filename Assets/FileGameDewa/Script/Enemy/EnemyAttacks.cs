using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    // Waktu dalam detik antara setiap serangan
    public float timeBetweenAttacks = 0.5f;

    // Jumlah health yang diambil per serangan.
    public int attackDamage = 10;


    // Reference to component
    Animator anim;
    GameObject player;
    PlayerHeal playerHeal;
    EnemyHeal enemyHeal;

    // Apakah pemain berada di dalam trigger collider dan dapat diserang.
    bool playerInRange;

    // Timer untuk menghitung hingga serangan berikutnya.
    float timer;

    void Awake()
    {
        // Mencari gameObject dengan tag "PlayerShoot"
        player = GameObject.FindGameObjectWithTag("PlayerShoot");

        // Mendapatkan komponen
        playerHeal = player.GetComponent<PlayerHeal>();
        enemyHeal = GetComponent<EnemyHeal>();
        anim = GetComponent<Animator>();
    }


    // Callback jika ada suatu object masuk ke dalam trigger
    void OnTriggerEnter(Collider other)
    {
        // Set player in range
        if(other.gameObject == player && other.isTrigger == false)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }


    // Callback jika ada suatu object yg keluar dari trigger
    void OnTriggerExit(Collider other)
    {
        // Set player not in range
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update()
    {
        // Tambahkan waktu sejak Pembaruan terakhir kali dipanggil ke penghitung waktu.
        timer += Time.deltaTime;

        // Jika penghitung waktu melebihi waktu antara serangan, 
        // pemain berada dalam jangkauan dan musuh ini masih hidup...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHeal.currentHealth > 0)
        {
            Attacks();
        }

        // Mentrigger animasi PlayerDead jika darah player kurang dari sama dengan 0
        if(playerHeal.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger("PlayerDead");
        }
    }

    void Attacks()
    {
        // Reset timer
        timer = 0f;

        // Taking Damage
        if(playerHeal.currentHealth > 0)
        {
            // ... damage the player.
            playerHeal.TakeDamage(attackDamage);
        }
    }
}

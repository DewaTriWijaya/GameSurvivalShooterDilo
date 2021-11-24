using UnityEngine;
using UnityEngine.AI;

public class EnemyHeal : MonoBehaviour
{
    // jumlah kesehatan musuh memulai permainan
    public int startingHealth = 100;

    // kesehatan saat ini yang dimiliki musuh
    public int currentHealth;

    // kecepatan musuh tenggelam di lantai saat mati
    public float sinkSpeed = 2.5f;

    // jumlah yang ditambahkan ke skor pemain saat musuh mati
    public int scoreValue = 10;

    // suara untuk dimainkan saat musuh mati
    public AudioClip deathClip;


    // reference to the animator
    Animator anim;

    // reference to the audio source
    AudioSource enemyAudio;

    // reference to the particle system that plays when the enemy is damaged
    ParticleSystem hitParticles;

    // reference to the capsule collider
    CapsuleCollider capsuleCollider;

    // apakah musuh sudah mati
    bool isDead;

    // apakah musuh sudah mulai tenggelam di lantai
    bool isSinking;

    void Awake()
    {
        // setting up the references
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // mengatur kesehatan saat ini ketika musuh pertama kali muncul
        currentHealth = startingHealth;
    }

    void Update()
    {
        // jika musuh harus tenggelam...
        if (isSinking)
        {
            // ...pindahkan musuh ke bawah dengan sinkSpeed per detik
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // jika musuh sudah mati...
        if (isDead)
            // ... tidak perlu menerima kerusakan jadi keluar
            return;

        // Play audio
        enemyAudio.Play();

        // kurangi health
        currentHealth -= amount;

        // ganti posisi particle
        hitParticles.transform.position = hitPoint;

        // play particle system
        hitParticles.Play();

        // dead jika health <= 0
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        // set isdead
        isDead = true;

        // SetCapcollider ke trigger
        capsuleCollider.isTrigger = true;

        // trigger play animation dead
        anim.SetTrigger("Dead");

        // play sound dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        // disable navmesh component
        GetComponent<NavMeshAgent>().enabled = false;

        // set rigidbody ke kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}

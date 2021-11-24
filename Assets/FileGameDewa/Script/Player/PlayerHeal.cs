using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeal : MonoBehaviour
{
    // Jumlah health pemain memulai permainan.
    public int startingHealth = 100;

    // health pemain saat ini.
    public int currentHealth;

    // Reference to the UI's health bar.
    public Slider healthSlider;

    // Referensi ke gambar untuk berkedip di layar saat terluka.
    public Image damageImage;

    // Klip audio untuk diputar saat pemain mati.
    public AudioClip deathClip;

    // Kecepatan damageImage akan memudar.
    public float flashSpeed = 5f;

    // Warna damageImage diatur ke, untuk berkedip.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMove playerMove;
    PlayerShoot playerShoot;

    bool isDead;
    bool damaged;


    void Awake()
    {
        // Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMove = GetComponent<PlayerMove>();
        playerShoot = GetComponent<PlayerShoot>();

        // Atur health awal pemain.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // Jika pemain baru saja terkena damage ...
        if (damaged)
        {
            // Merubah warna gambar menjadi value dari flashColor
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // Fade out damaged image.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // set damage to false
        damaged = false;
    }


    // fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Kurangi health saat ini dengan jumlah kerusakan.
        currentHealth -= amount;

        // Merubah tampilan dari health slider.
        healthSlider.value = currentHealth;

        // Memainkan suara ketika terkena damage.
        playerAudio.Play();

        // Memanggil method Death() jika darahnya kurang dari sama dengan 10 dan belu mati.
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    void Death()
    {
        // Setel tanda kematian agar fungsi ini tidak dipanggil lagi.
        isDead = true;

        // Matikan efek shoot yang tersisa.
        playerShoot.DisableEffects();

        // Beritahu animator bahwa pemain sudah mati.
        anim.SetTrigger("Die");

        // Atur sumber audio untuk memutar klip kematian dan memutarnya 
        //(ini akan menghentikan pemutaran suara yang menyakitkan).
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //Matikan skrip gerakan dan shoot.
        playerMove.enabled = false;
        playerShoot.enabled = false;
    }
}

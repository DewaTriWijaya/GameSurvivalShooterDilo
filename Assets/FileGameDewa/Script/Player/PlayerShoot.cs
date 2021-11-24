using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Kerusakan yang ditimbulkan oleh setiap peluru.
    public int damagePerShot = 20;

    // Waktu antara setiap tembakan.
    public float timeBetweenBullets = 0.15f;

    // Jarak yang bisa ditembakkan oleh pistol.
    public float range = 100f;


    // Timer untuk menentukan kapan harus menembak.
    float timer;

    // Sebuah sinar dari ujung pistol ke depan.
    Ray shootRay; // = new Ray();

    // Sebuah raycast hit untuk mendapatkan informasi tentang apa yang terkena.
    RaycastHit shootHit;

    // Lapisan topeng sehingga raycast hanya mengenai hal-hal di lapisan yang dapat ditembak.
    int shootableMask;

    // Reference to component
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;

    float effectsDisplayTime = 0.2f;


    void Awake()
    {
        // GetMask
        shootableMask = LayerMask.GetMask("Shootable");  

        // Mendapatkan Reference component
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }


    void Update()
    {
        // Tambahkan waktu sejak Pembaruan terakhir kali dipanggil ke penghitung waktu.
        timer += Time.deltaTime;

        // Jika tombol Fire1 sedang ditekan dan saatnya untuk menembak ...
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        // Jika penghitung waktu telah melebihi proporsi waktuBetweenBullets yang efeknya harus ditampilkan untuk...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }


    }

    public void DisableEffects()
    {
        // disable line renderer
        gunLine.enabled = false;

        // disable light
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        // play audio
        gunAudio.Play();

        // enable light
        gunLight.enabled = true;

        // play gun particle
        gunParticles.Stop();
        gunParticles.Play();

        // enable line renderer dan set first position
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // set posisi ray shoot dan direction
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // lakukan raycast jika mendeteksi id enemy hit apapun
        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // lakukan raycast hit hace component Enemyhealth
            EnemyHeal enemyHeal = shootHit.collider.GetComponent<EnemyHeal>();

            if(enemyHeal != null)
            {
                // lakukan take damage
                enemyHeal.TakeDamage(damagePerShot, shootHit.point);
            }

            // set line and position ke hit position
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            // set line and position ke range from barrel
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}

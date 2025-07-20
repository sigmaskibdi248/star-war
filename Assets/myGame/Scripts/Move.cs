using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public float moveSpeed = 25f;
    private UIManager uiManager;
    public Projectile laserPrefab;
    public int lives = 3;
    public Image[] hearts;
    //làm nhấp nháy khi bị trúng đạn
    private bool isInvincible = false;
    public float invincibilityDuration = 1.5f; // thời gian bất tử sau khi trúng đòn
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in scene!");
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float MoveX = Input.GetAxis("Horizontal");
        float MoveY = Input.GetAxis("Vertical");
        transform.position += new Vector3(MoveX, MoveY, 0) * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FireLaser();

        }
    }

    private void FireLaser()
    {
        if (AudioManager.Instance != null) {
            AudioManager.Instance.PlaySFX("ShootAudio");
            
        }
        Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, this.laserPrefab.transform.rotation);
    }


    // Kiểm tra va chạm vs đối tượng khác
    public void OnTriggerEnter2D(Collider2D other)
    {
         Debug.Log("s");
        if (other.gameObject.layer == LayerMask.NameToLayer("Invander") ||
        other.gameObject.layer == LayerMask.NameToLayer("Missle"))
        {
            TakeDamage();

        }
    }

    //      Hàm xử lí khi nhân vật bị trúng đạn
    public void TakeDamage()
    {
        if (isInvincible || lives <= 0) return;

        lives--;
        // Cập nhật giao diện hiển thị mạng sống N.vật
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        // kiểm tra mạng sống
        if (lives <= 0)
        {
            uiManager.ShowGameOver();
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX("GameOverAudio");
            }
        }
        else
        {
            // Coroutine để xử lí chế độ bất tử
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        float elapsed = 0f;
        bool visible = true;
        //Bắt đầu nhấp nháy
        while (elapsed < invincibilityDuration)
        {
            visible = !visible;
            spriteRenderer.enabled = visible;

            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }

        spriteRenderer.enabled = true; // bật lại nếu đang tắt
        isInvincible = false;

    }


}




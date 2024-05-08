using System.Collections;
using UnityEngine;


public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = .5f;
    
    [SerializeField] private float laserGrowAmount = 10f;
    [SerializeField] private int laserDamage = 40;


    [SerializeField] private float laserLastingTime = .8f;

    private bool isGrowing = true;
    private float laserRange;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserDirection();
        UpdateLaserRange(laserGrowAmount);
        Debug.Log("laser beam!");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player;
        if (collision.TryGetComponent<Player>(out player))
        {
            player.TakeDamage(laserDamage);
        }
    }

    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
        StartCoroutine(DestroyLaser());
    }


    private IEnumerator DestroyLaser()
    {

        yield return new WaitForSeconds(laserLastingTime);
        Destroy(gameObject);

        
    }

    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;

        while (spriteRenderer.size.y < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            // sprite 
            spriteRenderer.size = new Vector2(.1f,Mathf.Lerp(1f, laserRange, linearT));

            // collider
            capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, Mathf.Lerp(1f, laserRange, linearT));
            capsuleCollider2D.offset = new Vector2( capsuleCollider2D.offset.x, -Mathf.Lerp(-.5f, laserRange, linearT) / 2);
            yield return null;
        }

        //StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserDirection()
    {
        transform.up = Vector2.up;
        //Vector3 mousePosition = new Vector2(0f,7f);
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Vector2 direction = transform.position - mousePosition;
        //transform.up = -direction;
    }
}

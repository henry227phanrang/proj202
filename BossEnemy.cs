using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedNormalShot = 20f;

    [SerializeField] private float speedCircleShot = 10f;
    [SerializeField] private float HpValue = 100f;
    [SerializeField] private GameObject miniEnemy;
    [SerializeField] private float skillCoolDown = 2f;

    private float nextSkillTime = 0f;

    [SerializeField] private GameObject usbPrefabs;
    protected override void Update()
    {
        base.Update();
        if (Time.time >= nextSkillTime)
        {
            UseSkill();
        }
    }


    protected override void Die()
    {
        Instantiate(usbPrefabs,transform.position,Quaternion.identity);
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(enterDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(stayDamage);
        }
    }

    private void NormalShot()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedNormalShot);
        }
    }

    private void CircleShot()
    {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
            GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(bulletDirection * speedCircleShot);
        }
    }

    private void Recover(float hpAmount)
    {
        currentHp = Mathf.Min(currentHp + hpAmount, maxHp);
        UpdateHpBar();
    }

    private void CreateMiniEnemy()
    {
        Instantiate(miniEnemy, transform.position, Quaternion.identity);
    }

    private void Move()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
        }
    }

    private void SelectRandomSkill()
    {
        int randomSkill = Random.Range(0, 5);
        switch (randomSkill)
        {
            case 0:
                NormalShot();
                break;
            case 1:
                CircleShot();
                break;
            case 2:
                Recover(HpValue);
                break;
            case 3:
                CreateMiniEnemy();
                break;
            case 4:
                Move();
                break;
        }
    }

    private void UseSkill()
    {
        nextSkillTime = Time.time + skillCoolDown;
        SelectRandomSkill();
    }
}

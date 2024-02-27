using UnityEngine;

public class BulletHellWeapon : Weapon
{
    [SerializeField] private int bulletsAmount = 10;
    [SerializeField] private float startAngle = 90f;
    [SerializeField] private float endAngle = 270f;

    [SerializeField] private float[] startingAngles;
    
    LookUpTable<float, float> _sinTable;
    LookUpTable<float, float> _cosTable;

    public bool canShoot;

    // Start is called before the first frame update
    public override void Start()
    {
        canShoot = true;
        pool = new Pool<GenericBullet>(Factory, GenericBullet.Disable, GenericBullet.Active, startingBullets);
        InvokeRepeating("Shooting", time, repeatRate);
        _sinTable = new LookUpTable<float, float>(FactorySin);
        _cosTable = new LookUpTable<float, float>(FactoryCos);
    }

    float FactorySin(float value)
    {
        return Mathf.Sin((value * Mathf.PI) / 180f);
    }
    float FactoryCos(float value)
    {
        return Mathf.Cos((value * Mathf.PI) / 180f);
    }

    public override void Shooting()
    {
        if(!canShoot) return;
        
        for (int i = 0; i < startingAngles.Length; i++)
        {
            var num = Random.Range(0, startingAngles.Length);
            startAngle = startingAngles[num];
        }
        
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + _sinTable.ReturnValue(angle); 
            float bulDirY = transform.position.y + _cosTable.ReturnValue(angle);
            float bulDirZ = transform.position.z;

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, bulDirZ);
            Vector3 bulDir = (bulMoveVector - transform.position).normalized;

            var actualBullet = pool.AcquireObj();               
                actualBullet.SetBulletDirection(bulDir);
                SetPositionAndRotationOfThisBullet(actualBullet);
                
            angle += angleStep;
        }
    }
}

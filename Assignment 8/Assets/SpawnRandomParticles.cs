using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomParticles : MonoBehaviour
{

    public Vector2 topLeft;
    public Vector2 bottomRight;

    public GameObject projectile;

    [SerializeField]
    private float delay = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRandomParticle());   
    }

    //Probably not the best way but I am a sucker for IEunerators and this is the perfect excuse to use one
    IEnumerator SpawnRandomParticle()
    {
        Particle2D temp;
        GameObject spawnedProjectile;

        while (true)
        {
            Vector3 newPos = new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(topLeft.y, bottomRight.y), 0.0f);
            spawnedProjectile = Instantiate(projectile, newPos, Quaternion.identity);
            temp = spawnedProjectile.GetComponent<Particle2D>();
            temp.Velocity = new Vector2(0.0f, -1.0f);
            temp.Acceleration = new Vector2(0.0f, -6.0f);
            temp.Mass = 1.0f;

            ParticleManager.Instance.AddParticle(temp);
            GameManager.Instance.mObjects.Add(temp);

            yield return new WaitForSeconds(delay);
        }
    }
}

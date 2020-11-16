using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager Instance { get; private set; }
    public List<Particle2D> particles;
    private List<Particle2D> particlesToDelete;

    private void Awake()
    {
        particles = new List<Particle2D>();
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        particlesToDelete = new List<Particle2D>();
    }


    public void AddParticle(Particle2D particle)
    {
        particles.Add(particle);
    }

    public void DeleteParticle(Particle2D particle)
    {
        particles.Remove(particle);
    }

    public void Update()
    {
        foreach(var particle1 in particles)
        {
            foreach(var particle2 in particles)
            {
                if(particle1 != particle2 && particle1 != null && particle2 != null)
                {
                    if(CollisionDetector.Instance.DetectCollision(particle1, particle2))
                    {
                        if (!particlesToDelete.Contains(particle1)) particlesToDelete.Add(particle1);
                        if (!particlesToDelete.Contains(particle2)) particlesToDelete.Add(particle2);
                    }
                }
            }
        }

        foreach(var particle in particlesToDelete)
        {
            particles.Remove(particle);
            Destroy(particle.gameObject);
        }
        particlesToDelete.Clear();

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator : MonoBehaviour
{

    public static Integrator Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 Integrate(Vector2 loc, GameObject obj, bool shouldIgnoreForces)
    {
        Particle2D particle = obj.GetComponent<Particle2D>();
        Vector2 newLoc = loc;
        newLoc += (particle.Velocity * Time.deltaTime);

        Vector2 resultingAcc = particle.Acceleration;

        if (!shouldIgnoreForces)//accumulate forces here
        {
            resultingAcc += particle.AccumulatedForces * (1.0f / particle.Mass);
        }

        particle.Velocity += (resultingAcc * Time.deltaTime);
        float damping = Mathf.Pow(particle.DampingConstant, Time.deltaTime);
        particle.Velocity *= damping;

        clearAccumulatedForces(particle);
        return newLoc;
    }

    void clearAccumulatedForces(Particle2D particle)
    {
        particle.AccumulatedForces = Vector2.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    public float Mass;
    public Vector2 Velocity;
    public Vector2 Acceleration;
    public Vector2 AccumulatedForces;
    public float DampingConstant;
    public bool shouldIgnoreForces;
    public bool shouldIgnoreBouyancy;

    public Vector2 topLeft;
    public Vector2 bottomRight;

    [HideInInspector]
    public BouyancyForceGenerator mBouyancyForce;

    private void Start()
    {
        if (!shouldIgnoreBouyancy)
        {
            mBouyancyForce = new BouyancyForceGenerator(true, gameObject, -6, 50, 0);
            GameManager.Instance.mForceManager.addForceGenerator(mBouyancyForce);
        }
    }

    void FixedUpdate()
    {
        transform.position = Integrator.Instance.Integrate(transform.position, gameObject, shouldIgnoreForces);
    }

    public void addForce(Vector2 temp)
    {
        AccumulatedForces += temp;
    }

    public bool inBounds()
    {
        bool onScreen = true;
        if(gameObject != null)
        if (transform.position.x < topLeft.x || transform.position.x > bottomRight.x || transform.position.y > topLeft.y || transform.position.y < bottomRight.y)
            onScreen = false;
        return onScreen; 
    }

}

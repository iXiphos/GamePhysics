using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{

    public Particle2D particle1;
    public Particle2D particle2;
    public float RestitutionCoefficient;
    public Vector2 ContactNormal;
    public float Penetration;
    public Vector2 MoveDirection1;
    public Vector2 MoveDirection2;

    public Particle2DContact(GameObject gameObject1, GameObject gameObject2, float restitutionCoefficient, Vector2 contactNormal, float penetration, Vector2 move1, Vector2 move2)
    {
        particle1 = gameObject1.GetComponent<Particle2D>();
        particle2 = gameObject2.GetComponent<Particle2D>();
        RestitutionCoefficient = restitutionCoefficient;
        ContactNormal = contactNormal.normalized;
        Penetration = penetration;
        MoveDirection1 = move1;
        MoveDirection2 = move2;
    }

    public void resolve()
    {
        resolveVelocity();
        resolveInterpenetration();
    }

    public float calculateSeparatingVelocity()
    {
        Vector2 relativeVel = particle1.Velocity;
	    if (particle2 != null)
	    {
	        relativeVel -= particle2.Velocity;
	    }
        return Vector2.Dot(relativeVel, ContactNormal);
    }

    public void resolveVelocity()
    {
        float separatingVel = calculateSeparatingVelocity();
        if (separatingVel > 0.0f)//already separating so need to resolve
            return;

        float newSepVel = -separatingVel * RestitutionCoefficient;

        Vector2 velFromAcc = particle1.Acceleration;
        if (particle1 != null)
            velFromAcc -= particle2.Acceleration;
        float accCausedSepVelocity = Vector2.Dot(velFromAcc, ContactNormal) * Time.deltaTime;

        if (accCausedSepVelocity < 0.0f)
        {
            newSepVel += RestitutionCoefficient * accCausedSepVelocity;
            if (newSepVel < 0.0f)
                newSepVel = 0.0f;
        }

        float deltaVel = newSepVel - separatingVel;

        float totalInverseMass = 1.0f/particle1.Mass;
        if (particle2 != null)
            totalInverseMass += 1.0f / particle2.Mass;

            if (totalInverseMass <= 0)//all infinite massed objects
            return;

        float impulse = deltaVel / totalInverseMass;
        Vector2 impulsePerIMass = ContactNormal * impulse;

        Vector2 newVelocity = particle1.Velocity + impulsePerIMass * (1.0f / particle1.Mass);
        particle1.Velocity = newVelocity;
        if (particle2 != null)
        {
            newVelocity = particle2.Velocity + impulsePerIMass * (1.0f / particle2.Mass);
            particle2.Velocity = newVelocity;
        }
    }

    public void resolveInterpenetration()
    {
        if (Penetration <= 0.0f)
            return;

        float totalInverseMass = (1.0f / particle1.Mass);
        if (particle2 != null)
            totalInverseMass += (1.0f / particle2.Mass);

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        Vector2 movePerIMass = ContactNormal * (Penetration / totalInverseMass);

        MoveDirection1 = movePerIMass * (1.0f / particle1.Mass);
        if (particle2 != null)
            MoveDirection2 = movePerIMass * -(1.0f / particle2.Mass);
        else
            MoveDirection2 = Vector2.zero;

        Vector2 newPosition = new Vector2(particle1.transform.position.x, particle1.transform.position.y) + MoveDirection1;
        particle1.transform.position = newPosition;
        if (particle2 != null)
        {
            newPosition = new Vector2(particle2.transform.position.x, particle2.transform.position.y) + MoveDirection2;
            particle2.transform.position = newPosition;
        }
    }
}

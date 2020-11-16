using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactResolver : MonoBehaviour
{

    public static ContactResolver Instance { get; private set; }

    int iterationsUsed = 0;
    public int numIterations = 10;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResolveContacts(List<Particle2DContact> contacts)
    {
        if (contacts.Count == 0)
            return;

        int i;
        iterationsUsed = 0;
        while (iterationsUsed < numIterations)
        {
            float max = float.MaxValue;
            int numContacts = contacts.Count;
            int maxIndex = numContacts;
            i = 0;
            foreach (Particle2DContact contact in contacts)
            {
                float sepVel = contact.calculateSeparatingVelocity();
                if (sepVel < max && (sepVel < 0.0f || contact.Penetration > 0.0f))
                {
                    max = sepVel;
                    maxIndex = i;
                }
                i++;
            }
            if (maxIndex == numContacts)
                break;

            contacts[maxIndex].resolve();

            foreach (Particle2DContact contact in contacts)
            {
                if (contact.particle1 == contacts[maxIndex].particle1)
                    contact.Penetration -= Vector2.Dot(contacts[maxIndex].MoveDirection1, contact.ContactNormal);
                else if (contact.particle1 == contacts[maxIndex].particle2)
                    contact.Penetration -= Vector2.Dot(contacts[maxIndex].MoveDirection2, contact.ContactNormal);

                if (contact.particle2 == contacts[maxIndex].particle1)
                    contact.Penetration -= Vector2.Dot(contacts[maxIndex].MoveDirection1, contact.ContactNormal);
                else if (contact.particle2 == contacts[maxIndex].particle2)
                    contact.Penetration -= Vector2.Dot(contacts[maxIndex].MoveDirection2, contact.ContactNormal);
            }
            iterationsUsed++;
        }
        contacts.Clear();
    }
}


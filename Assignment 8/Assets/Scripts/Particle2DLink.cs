using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DLink : MonoBehaviour
{
    public Particle2D particle1;
    public Particle2D particle2;

    public float mLength;

    public Particle2DLink(GameObject obj1, GameObject obj2, float length)
    {
        particle1 = obj1.GetComponent<Particle2D>();
        particle2 = obj2.GetComponent<Particle2D>();
        mLength = length;
    }

    public void CreateContacts(List<Particle2DContact> contacts)
    {
        if (particle1 == null || particle2 == null)
            return;

        Vector2 temp = particle1.transform.position - particle2.transform.position;

        float length = temp.magnitude;
        if (length == mLength)
            return;

        Vector2 normal = particle1.transform.position - particle2.transform.position;
        normal = normal.normalized;

        float penetration;
        if (length < mLength)
        {
            penetration = (mLength - length) / 1000.0f;

            Particle2DContact contact = new Particle2DContact(particle1.gameObject, particle2.gameObject, 0, -normal, penetration, Vector2.zero, Vector2.zero);
            contacts.Add(contact);
        }
        else
        {
            penetration = (length - mLength) / 1000.0f;
            Particle2DContact contact = new Particle2DContact(particle1.gameObject, particle2.gameObject, 0, -normal, penetration, Vector2.zero, Vector2.zero);
            contacts.Add(contact);
        }
    }
}

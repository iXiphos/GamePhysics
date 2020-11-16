using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public static CollisionDetector Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public bool DetectCollision(Particle2D obj1, Particle2D obj2)
    {
        bool result = false;
        float distance = Vector2.Distance(new Vector2(obj1.transform.position.x, obj1.transform.position.y), new Vector2(obj2.gameObject.transform.position.x, obj2.gameObject.transform.position.y));
        if (distance < (obj1.GetComponent<Particle2D>().radius + obj2.GetComponent<Particle2D>().radius))
        {
            result = true;
        }
        return result;
    }
}

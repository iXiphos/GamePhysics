using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawn : MonoBehaviour
{
    float radius = 1;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.mObjects != null)
        {
            for(int i = 0; i < GameManager.Instance.mObjects.Count; i++)
            {
                if (GameManager.Instance.mObjects[i] != null)
                {
                    if (GameManager.Instance.mObjects[i].tag == "PlayerProjectile")
                    {
                        GameObject projectile = GameManager.Instance.mObjects[i].gameObject;
                        float distance = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(projectile.gameObject.transform.position.x, projectile.gameObject.transform.position.y));
                        if (distance < radius)
                        {
                            Destroy(gameObject);
                            break;
                        }
                    }
                }
            }
        }
    }
}

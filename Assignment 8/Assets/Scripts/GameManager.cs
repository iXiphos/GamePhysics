using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public ForceManager mForceManager;
    public List<Particle2DContact> mContacts;
    public List<Particle2D> mObjects;

    public GameObject Target;
    GameObject aliveTarget;

    public int score;
    public Text scoreText;

    public void Awake()
    {
        mForceManager = new ForceManager();
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        RespawnTarget();
    }

    private void Update()
    {
        mForceManager.UpdateGenerators();
        if(aliveTarget == null)
        {
            RespawnTarget();
            score++;
        }
        scoreText.text = "Score: " + score;
        mContacts.Clear();
        ContactResolver.Instance.ResolveContacts(mContacts);
        UpdateObjects();
    }


    public void RespawnTarget()
    {
        Vector3 carPos = new Vector3(Random.Range(-6f, 6f), transform.position.y, transform.position.z);
        aliveTarget = Instantiate(Target, carPos, transform.rotation);
    }

    public void UpdateObjects()
    {
        for(int i = 0; i < mObjects.Count; i++)
        {
            if (mObjects[i] != null)
                if (!mObjects[i].inBounds())
                {
                    mForceManager.removeForceGenerator(mObjects[i].mBouyancyForce);
                    Destroy(mObjects[i].gameObject, 1.0f);
                    mObjects.RemoveAt(i);
                    mObjects.TrimExcess();
                }
        }
    }

}

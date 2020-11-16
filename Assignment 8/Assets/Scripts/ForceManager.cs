using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    public List<ForceGenerator> mForceGenerators = new List<ForceGenerator>();


    public void addForceGenerator(ForceGenerator generator)
    {
        mForceGenerators.Add(generator);
    }
    public void removeForceGenerator(ForceGenerator generator)
    {
        mForceGenerators.Remove(generator);
    }

    public void UpdateGenerators()
    {
        for (int i = 0; i < mForceGenerators.Count; i++)
        {
            mForceGenerators[i].UpdateForce();
        }
    }

}

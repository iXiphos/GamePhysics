using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator
{
    protected bool mShouldEffectAll = true;

    public ForceGenerator(bool effectAll)
    {
        mShouldEffectAll = effectAll;
    }

    public virtual void UpdateForce()
    {

    }

    public bool EffectAll()
    {
        return mShouldEffectAll;
    }
}

public class SpringForceGenerator : ForceGenerator
{
    Particle2D mObj1, mObj2;
    float mSpringConstant;
    float mRestLength;

    public SpringForceGenerator(GameObject obj1, GameObject obj2, float springConstant, float restLength, bool effectAll) : base(effectAll)
    {
        mObj1 = obj1.GetComponent<Particle2D>();
        mObj2 = obj2.GetComponent<Particle2D>();
        mSpringConstant = springConstant;
        mRestLength = restLength;
    }

    public override void UpdateForce()
    {
        if (mObj1 == null || mObj2 == null)
            return;

        Vector2 pos1 = mObj1.transform.position;
        Vector2 pos2 = mObj2.transform.position;

        Vector2 diff = pos1 - pos2;
        float dist = diff.magnitude;

        float magnitude = dist - mRestLength;
        magnitude *= mSpringConstant;

        diff = diff.normalized;
        diff *= magnitude;

        mObj1.addForce(diff);
        mObj2.addForce(-diff);
    }
}

public class BouyancyForceGenerator : ForceGenerator
{
    Particle2D mObj;
    float mMaxDepth;
    float mVolume;
    float mWaterHeight;
    float mDensity;

    public BouyancyForceGenerator(bool effectAll, GameObject obj, float depth, float volume, float waterHeight, float density = 0.5f) : base(effectAll)
    {
        mObj = obj.GetComponent<Particle2D>();
        mMaxDepth = depth;
        mVolume = volume;
        mWaterHeight = waterHeight;
        mDensity = density;
    }

    public override void UpdateForce()
    {
        if (mObj == null)
            return;

        float currentDepth = mObj.transform.position.y;
        Vector2 bouyancyForce = Vector2.zero;

        if (currentDepth <= mWaterHeight)
        {
            if (currentDepth <= mMaxDepth)
            {
                bouyancyForce.y = mDensity * mVolume;
                mObj.addForce(bouyancyForce);
            }
            else
            {
                bouyancyForce.y = mDensity * mVolume * (-(currentDepth - mMaxDepth - mWaterHeight)) / (2 * mMaxDepth);
                mObj.addForce(bouyancyForce);
            }
        }
    }
}


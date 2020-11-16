using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType{
    Spring,
    Rod
}

public class Fire : MonoBehaviour
{

    public weaponType currWeapon;

    public GameObject projectile;

    public List<Particle2DLink> mLinks;

    // Start is called before the first frame update
    void Start()
    {
        currWeapon = weaponType.Spring;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(currWeapon == weaponType.Spring)
                currWeapon = weaponType.Rod;
            else
                currWeapon = weaponType.Spring;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currWeapon == weaponType.Spring)
                shootSpring();
            else
                shootRod();
        }
        foreach(var link in mLinks)
        {
            link.CreateContacts(GameManager.Instance.mContacts);
        }
    }

    void shootSpring()
    {
        float speed = 10.0f;
        Vector2 gravity = new Vector2(0.0f, -6.0f);
        GameObject proj = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        Particle2D temp = proj.GetComponent<Particle2D>();
        temp.Velocity = transform.right * speed;
        temp.Acceleration = gravity;
        temp.Mass = 1.0f;
        GameManager.Instance.mObjects.Add(temp);

        GameObject proj1 = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        temp = proj1.GetComponent<Particle2D>();
        temp.Velocity = transform.right * speed;
        temp.Acceleration = gravity;
        temp.Mass = 3.0f;
        GameManager.Instance.mObjects.Add(temp);

        SpringForceGenerator springGenerator = new SpringForceGenerator(proj, proj1, 1.0f, 50.0f, true);
        GameManager.Instance.mForceManager.addForceGenerator(springGenerator);
    }

    void shootRod()
    {
        float speed = 10.0f;
        Vector2 gravity = new Vector2(0.0f, - 2.0f);

        Debug.LogWarning("the fucks");

        GameObject proj = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        Particle2D temp = proj.GetComponent<Particle2D>();
        temp.Velocity = transform.right * speed;
        temp.Acceleration = gravity;
        temp.Mass = 1.0f;
        GameManager.Instance.mObjects.Add(temp);

        GameObject proj1 = Instantiate(projectile, (gameObject.transform.position + new Vector3(2.5f, 2.5f)), gameObject.transform.rotation);
        Debug.LogError(proj1.transform.position);
        Debug.LogError(proj.transform.position);
        temp = proj1.GetComponent<Particle2D>();
        temp.Velocity = transform.right * (speed + 2.0f);
        temp.Acceleration = gravity;
        temp.Mass = 1.0f;
        GameManager.Instance.mObjects.Add(temp);

        Particle2DLink link = new Particle2DLink(proj, proj1, 1.0f);
        mLinks.Add(link);

    }
}

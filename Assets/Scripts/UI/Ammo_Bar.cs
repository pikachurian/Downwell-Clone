using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Bar : MonoBehaviour
{
    public Transform bulletGUItransform;
    public float bulletGUIYspace;
    public GameObject bulletGUIprefab;
    private List<GameObject> GUIBullets = new List<GameObject>();
    void Start()
    {
        
        SetBulletGUI(8);
    }

    
    void Update()
    {
        
    }

    public void SetBulletGUI( int count )
    {
        if (GUIBullets.Count > 0)
        {
            for (int i = 0; i < GUIBullets.Count; i++)
            {
                Destroy(GUIBullets[0]);
                GUIBullets.RemoveAt(0);
            }

            if (GUIBullets.Count > 0)
            {
                Destroy(GUIBullets[0]);
                GUIBullets.RemoveAt(0);
            }
           
        }

        if (count == 0)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject bulletGUI = Instantiate(bulletGUIprefab);
            bulletGUI.transform.position = bulletGUItransform.position;
            bulletGUI.transform.position +=  new Vector3(0, i * bulletGUIYspace,0);
            GUIBullets.Add(bulletGUI);  
            bulletGUI.transform.SetParent(this.transform);
        }

    }

    public void ShootBulletGUI()
    { 
        
    }

}

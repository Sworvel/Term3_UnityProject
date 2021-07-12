using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum CollectibleType
    {
        LOG,
        ITEM2,
        ITEM3,
    }

    public CollectibleType currentCollectible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
       switch (currentCollectible)
       {
           case CollectibleType.LOG:
               print("fukc");
               break;

           case CollectibleType.ITEM2:
               
               break;

           case CollectibleType.ITEM3:
               
               break;
       }
       
       Destroy(gameObject);
      
    }
}

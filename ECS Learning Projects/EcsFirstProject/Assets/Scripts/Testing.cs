using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

public class Testing : MonoBehaviour
{
    private void Start()
    {
        //Create an EntityManager to Create Modify and Destroy Entities
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        //Archtype is Collection of the Components into a single variable for usage
        EntityArchetype SimpleArchType = entityManager.CreateArchetype(typeof(LevelComponent),
                                      typeof(Translation) , typeof (Rotation) , typeof(Scale));

        NativeArray <Entity> entities = new NativeArray<Entity>(50 , Allocator.Temp);
        //Creating Entities using EntityManager
        entityManager.CreateEntity(SimpleArchType , entities);

        //entityManager.SetComponentData(entity , new LevelComponent {level = 10});
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class LevelUpdateSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach(
            (ref LevelComponent levelcomponent) =>
            {
                levelcomponent.level += 1 * Time.DeltaTime;
            }
            );
    }
}

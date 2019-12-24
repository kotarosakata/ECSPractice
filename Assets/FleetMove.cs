using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class FleetMove : ComponentSystem
{
    private float speed = 100f;
    // Start is called before the first frame update
    protected override void OnCreate()
    {
        base.OnCreate();

        this.query = GetEntityQuery(new EntityQueryDesc
        {
            All = new[] { ComponentType.ReadWrite<Translation>() },
        });
    }

    protected override void OnUpdate()
    {
        var time = Time.realtimeSinceStartup;
        Entities.ForEach((ref Translation translation) => { translation.Value.z += speed*Time.deltaTime; });
    }

    EntityQuery query;
}

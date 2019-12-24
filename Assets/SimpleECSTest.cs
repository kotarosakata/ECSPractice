using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class SimpleECSTest : MonoBehaviour
{
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;
    void Start()
    {
        CreateCube();
    }

    void CreateCube()
    {
        var manager = World.Active.EntityManager;
        // Entity が持つ Components を設計
        var archetype = manager.CreateArchetype(
            ComponentType.ReadWrite<LocalToWorld>(), 
            ComponentType.ReadWrite<Translation>(),
            ComponentType.ReadOnly<RenderMesh>());
        // 上記の Components を持つ Entity を作成
        var entity = manager.CreateEntity(archetype);
        // Entity の Component の値をセット（位置）
     //   manager.SetComponentData(entity, new Translation() { Value = new float3(0, 1, 0) });

        // キューブオブジェクトの作成

        
        // Entity の Component の値をセット（描画メッシュ）
       

       
       
        const int SIDE = 10;
        const int xSIDE = 100;
        using (NativeArray<Entity> entities = new NativeArray<Entity>(SIDE * xSIDE*SIDE, Allocator.Temp, NativeArrayOptions.UninitializedMemory))
        {
            // Prefab Entity をベースに 10000 個の Entity を作成
            manager.Instantiate(entity, entities);
            // 平面に敷き詰めるように Translation を初期化
            for (int x = 0; x < xSIDE; x++)
            {
                for (int y = 0; y < SIDE; y++)
                {
                    for (int z = 0; z < SIDE; z++)
                    {
                        int index = x + y * xSIDE + z * SIDE * xSIDE;
                        manager.SetComponentData(entities[index], new Translation
                        {
                            Value = new float3(x*100, y*60, z*200)
                        });
                        manager.SetSharedComponentData(entities[index], new RenderMesh()
                        {
                            mesh =_mesh,
                            material =_material,
                            subMesh = 0,
                            castShadows = UnityEngine.Rendering.ShadowCastingMode.Off,
                            receiveShadows = false
                        });
                    }

                }
            }
        }
        
        // キューブオブジェクトの削除
    }
}
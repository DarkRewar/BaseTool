using UnityEngine;

namespace BaseTool
{
    [AddComponentMenu("BaseTool/Core/GameObject Pool")]
    public class GameObjectPool : GenericObjectPool<GameObject>
    {
        protected override void ReleasePooledObject(GameObject obj)
        {
            base.ReleasePooledObject(obj);
            
            obj.transform.SetParent(transform);
        }
    }
}
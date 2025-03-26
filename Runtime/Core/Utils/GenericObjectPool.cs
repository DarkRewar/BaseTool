using UnityEngine;
using UnityEngine.Pool;

namespace BaseTool
{
    [HelpURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#genericobjectpool")]
    public abstract class GenericObjectPool<T> : MonoBehaviour where T : Object
    {
        [SerializeField] protected T _prefab;

        [SerializeField] protected bool _collectionCheck = true;

        [SerializeField] protected int _defaultCapacity = 10;

        [SerializeField] protected int _maxSize = 10000;

        protected ObjectPool<T> _objectPool;

        private void Awake()
        {
            _objectPool = new ObjectPool<T>(
                CreatePooledObject,
                GetPooledObject,
                ReleasePooledObject,
                DestroyPooledObject,
                _collectionCheck,
                _defaultCapacity,
                _maxSize);
        }

        protected virtual T CreatePooledObject()
        {
            T obj = Instantiate(_prefab, transform);
            return obj;
        }

        protected virtual void GetPooledObject(T obj)
        {
        }

        protected virtual void ReleasePooledObject(T obj)
        {
        }

        protected virtual void DestroyPooledObject(T obj)
        {
            Destroy(obj);
        }

        public T Get() => _objectPool.Get();

        public void Release(T obj) => _objectPool.Release(obj);
    }

    public abstract class GameObjectPool<T> : GenericObjectPool<T> where T : Component
    {
        protected override T CreatePooledObject()
        {
            T obj = base.CreatePooledObject();
            obj.gameObject.SetActive(false);
            return obj;
        }

        protected override void GetPooledObject(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected override void ReleasePooledObject(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
        }

        protected override void DestroyPooledObject(T obj)
        {
            Destroy(obj.gameObject);
        }
    }
}
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(AudioSource))]
public class SFXPoolObject : MonoBehaviour
{
    private AudioSource audioSource;
    public ObjectPool<AudioSource> pool;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            pool.Release(audioSource);
        }
    }
}

public class SFXPool : MonoBehaviour
{
    [SerializeField] private GameObject audioPrefab;
    [SerializeField] private int startingPoolSize = 3;
    [SerializeField] private int maxPoolSize = 10;

    private ObjectPool<AudioSource> pool;
    public ObjectPool<AudioSource> Pool
    {
        get
        {
            if (pool == null)
                pool = new ObjectPool<AudioSource>(CreatePooledItem, OnGetFromPool,
                       OnReleaseToPool, OnDestroyPoolObject, true, startingPoolSize, maxPoolSize);
            return pool;
        }
    }

    AudioSource CreatePooledItem()
    {
        var go = Instantiate(audioPrefab, transform);
        var audioSource = go.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No audio source found on audioPrefab", gameObject);
            return null;
        }

        var po = go.AddComponent<SFXPoolObject>();
        po.pool = this.pool;

        return audioSource;
    }

    void OnReleaseToPool(AudioSource audioSource)
    {
        audioSource.gameObject.SetActive(false);
    }

    void OnGetFromPool(AudioSource audioSource)
    {
        audioSource.gameObject.SetActive(true);
    }

    void OnDestroyPoolObject(AudioSource audioSource)
    {
        Destroy(audioSource.gameObject);
    }
}

using UnityEngine;


public abstract class BasePowerup : MonoBehaviour, IPowerup
{
    public PowerupType type;
    public bool spawned = false;
    protected bool consumed = false;
    protected bool goRight = true;
    protected Rigidbody2D rigidBody;

    // base methods

    protected virtual void Awake()
    {
        GameManager.instance.gameRestart.AddListener(DestroyPowerup);
    }
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        SpawnPowerup();
    }

    // interface methods
    // 1. concrete methods
    public PowerupType powerupType
    {
        get // getter
        {
            return type;
        }
    }

    public bool hasSpawned
    {
        get // getter
        {
            return spawned;
        }
    }

    public void DestroyPowerup()
    {
        Destroy(this.gameObject);
    }

    // 2. abstract methods, must be implemented by derived classes
    public abstract void SpawnPowerup();
    public abstract void ApplyPowerup(MonoBehaviour i);
}

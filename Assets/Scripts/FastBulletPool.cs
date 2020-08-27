public class FastBulletPool : BaseBulletPool
{
    private static FastBulletPool instance;

    public static FastBulletPool Instance { get => instance; private set => instance = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override Bullet RetrieveItemFromFactory()
    {
        return FastBulletFactory.Instance.CreateItem();
    }
}
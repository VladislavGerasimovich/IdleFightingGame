using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private int _maxCount;

    public int Count => _count;

    private void Awake()
    {
        _count = _maxCount;
    }

    public void Set(int value)
    {
        _count -= value;
    }

    public void Restore()
    {
        _count = _maxCount;
    }
}

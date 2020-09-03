public interface IPool<T>
{
    void Fill();

    T GetObject();

    void Recycle(T poolObject);
}
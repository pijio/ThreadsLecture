namespace ThreadsLecture;

public class Deadlocks
{
    public void AntiDeadlockExample()
    {
        var resourceX = new object();
        var resourceY = new object();
        var resourceZ = new object();

        var threadA = new Thread(() =>
        {
            lock (resourceZ)
            {
                lock (resourceX)
                {
                    // Работа с ресурсом X
                }
                lock (resourceY)
                {
                    // Работа с ресурсом Y
                }
            }
        });

        var threadB = new Thread(() =>
        {
            lock (resourceZ)
            {
                lock (resourceY)
                {
                    // Работа с ресурсом Y
                }
                lock (resourceX)
                {
                    // Работа с ресурсом X
                }
            }
        });

        threadA.Start();
        threadB.Start();

        threadA.Join();
        threadB.Join();
    }
}
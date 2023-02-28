/// <summary>
/// Пример использования событийно-ориентированного программирования для callback в главный поток
/// </summary>
public class MyWorker
{
    public event WorkerHandler Completed;

    public void DoWork()
    {
        // выполнение фоновой операции
        
        Thread.Sleep(2000);

        // генерация события Completed
        Completed?.Invoke(this, "Задача успешно выполнена!");
    }

    public delegate void WorkerHandler(object sender, string message);
}

/// <summary>
/// Условный класс UI
/// </summary>
public class MainForm
{
    public void button1_Click()
    {
        MyWorker worker = new MyWorker();
        worker.Completed += Worker_Completed;

        Thread thread = new Thread(worker.DoWork);
        thread.Start();
    }

    private void Worker_Completed(object sender, string message)
    {
        Console.WriteLine(message);
    }
}

/// <summary>
/// Пример передачи экземпляра делегата для callback в главный
/// </summary>
public class MyWorker2
{
    private Action<string> callback;

    public MyWorker2(Action<string> callback)
    {
        this.callback = callback;
    }

    public void DoWork()
    {
        // выполнение фоновой операции
        Thread.Sleep(2000);

        // вызов колбэка
        
        callback("Задача выполнена успешно");
    }
}

/// <summary>
/// Условный класс UI2
/// </summary>
public class MainForm2
{
    public void button1_Click()
    {
        var worker = new MyWorker2((message) => Console.WriteLine(message));

        var thread = new Thread(worker.DoWork);
        thread.Start();
    }
}
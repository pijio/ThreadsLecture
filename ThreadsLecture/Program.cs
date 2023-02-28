namespace ThreadsLecture
{
    public class Program
    {
        static Mutex mutex = new Mutex(); // создаем мьютекс
        static Semaphore semaphore = new Semaphore(2, 2); // создаем семафор с максимальным количеством потоков 2
        
        public static void Main(string[] args)
        {
            #region Mutex usage
            var t1 = new Thread(WriteToFile);
            var t2 = new Thread(WriteToFile);
            t1.Start(1);
            t2.Start(2);
            #endregion
            #region Semaphore usage
            for (int i = 1; i <= 10; i++) // создаем 10 потоков
            {
                var t = new Thread(() => WriteToFile2(i));
                t.Start();
            }

            Console.ReadLine();
            #endregion
        }
        
        static void WriteToFile(object id)
        {
            mutex.WaitOne(); // ожидаем доступа к мьютексу
            using (StreamWriter writer = new StreamWriter("data.txt", true))
            {
                writer.WriteLine($"{DateTime.Now}: Thread {id} - {new Random().Next(1, 100)}");
            }
            mutex.ReleaseMutex(); // освобождаем мьютекс
        }
        
        static void WriteToFile2(int threadId)
        {
            semaphore.WaitOne(); // запрашиваем разрешение на использование ресурса (в данном случае - файла)
            try
            {
                using (StreamWriter sw = new StreamWriter("output.txt", true)) // открываем файл на дозапись
                {
                    var rand = new Random();
                    var randomNumber = rand.Next(100);
                    sw.WriteLine($"{randomNumber}-{threadId}"); // записываем случайное число и Id потока
                }
            }
            finally
            {
                semaphore.Release(); // освобождаем ресурс (в данном случае - семафор)
            }
        }
    }
}
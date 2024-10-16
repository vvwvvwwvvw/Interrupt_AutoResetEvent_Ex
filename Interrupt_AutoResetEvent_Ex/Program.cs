namespace Interrupt_AutoResetEvent_Ex
{

    class Lock
    {
        AutoResetEvent ARE = new AutoResetEvent(true); // true available, fales lock;
        //Manua IReset MRE = new Manua IRestEvent(true); // 수동으로 문 닫기
        public void Acquire()
        {
            ARE.WaitOne(); // 입장 시도
        }
        public void Release()
        {
            ARE.Set(); // bool = true; 문을 연다
        }
    }
    class Program
    {
        static int num = 0;
        static Lock _lock = new Lock();
        // static Mutex _Lock = new Mutex();
        static void Thread_1()
        {
            for (int i = 0; i < 10000; i++)
            {
                //_lock.WaitOne(); // (locked == true);
                _lock.Acquire();
                num += 1;
                //_lock.ReleaseMutex();; //(locked == false) lock 해제
            }
        }
        static void Thread_2()
        {
            for (int i = 0; i < 10000; ++i)
            {
                _lock.Acquire();
                num -= 1;
                _lock.Release();
            }
        }
        static void Main(string[] args)
        {
         Task t1 = new Task(Thread_1);  
         Task t2 = new Task(Thread_2);

            t1.Start();
            t2.Start();
            Task.WaitAll(t1 , t2);

            Console.WriteLine(num);
        }
    }
}

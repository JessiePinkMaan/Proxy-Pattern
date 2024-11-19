namespace Proxy_Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            ISubject proxy = new Proxy();

            Console.WriteLine(proxy.Request("allowed ae86")); 
            Console.WriteLine(proxy.Request("allowed ae86")); 

            Console.WriteLine(proxy.Request("haval")); // Доступ запрещен!

            System.Threading.Thread.Sleep(11000); // ждем чтобы кеш истек

            Console.WriteLine(proxy.Request("allowed ae86")); // Обработает запрос заново, так как кэш истек
        }
    }
}
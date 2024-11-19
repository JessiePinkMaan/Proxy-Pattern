using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy_Pattern
{
    public class Proxy : ISubject
    {
        private readonly RealSubject rSubt; //ссылка на риал обьект 
        private readonly Dictionary<string, (string response, DateTime expiration)> _cache;
        private readonly TimeSpan cacheT; //как долго будет храниться резкльтат 

        public Proxy()
        {
            rSubt = new RealSubject();
            _cache = new Dictionary<string, (string response, DateTime expiration)>();
            cacheT = TimeSpan.FromSeconds(10); // kэш на 10 секунд
        }

        public string Request(string request)
        {
            
            if (!HasAccess(request))// Проверка прав доступа
            {
                return "Доступ запрещен!";
            }

            // поиск   кэша
            if (_cache.TryGetValue(request, out var cachedResponse))
            {
                if (DateTime.UtcNow < cachedResponse.expiration)//если текущее время меньше времени истечения возвращается ответ
                {
                    return cachedResponse.response; 
                }
                else
                {
                    _cache.Remove(request); // очищаем устаревший кэш
                }
            }

           
            var response = rSubt.Request(request);

            // кэшируем результат
            _cache[request] = (response, DateTime.UtcNow.Add(cacheT));

            return response;
        }

        private bool HasAccess(string request)
        {
      
            return request.StartsWith("allowed"); //  запросы начинающиеся с "allowed" получают доступ 
        }
    }
}

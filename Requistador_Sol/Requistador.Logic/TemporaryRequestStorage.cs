using Requistador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Requistador.Logic
{
    public static class TemporaryRequestStorage
    {
        private static object _padlock = new object();

        private static HashSet<AppRequest> _pendingRequests = new HashSet<AppRequest>();

        public static List<AppRequest> GetAllRequests()
        {
            lock(_padlock)
            {
                return _pendingRequests.ToList();
            }
        }

        public static void AddRequest(AppRequest request)
        {
            lock(_padlock)
            {
                _pendingRequests.Add(request);
            }
        }

        public static void RemoveRequest(AppRequest request)
        {
            lock (_padlock)
            {
                _pendingRequests.Remove(request);
            }
        }

        public static void RemoveAllRequest()
        {
            lock (_padlock)
            {
                _pendingRequests.Clear();
            }
        }
    }
}

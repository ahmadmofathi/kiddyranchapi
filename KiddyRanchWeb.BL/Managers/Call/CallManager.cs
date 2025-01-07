using KiddyRanchWeb.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class CallManager : ICallManager
    {
        private readonly ICallRepo _callRepo;

        public CallManager(ICallRepo callRepo)
        {
            _callRepo = callRepo;
        }
        public IEnumerable<CallDTO> GetCalls()
        {
            IEnumerable<Call> callsDB = _callRepo.GetCalls();
            return callsDB.Select(u => new CallDTO
            {
                callId = u.callId,
                calledId = u.calledId,
                callDate = u.callDate,
                callDescription = u.callDescription,
                callerName = u.callerName,
                creationDate = u.creationDate,
                updatedDate = u.updatedDate,
            });
        }
        public IEnumerable<CallDTO> GetHisCalls(string calledId)
        {
            IEnumerable<Call> callsDB = _callRepo.GetHisCalls(calledId);
            return callsDB.Select(u => new CallDTO
            {
                callId = u.callId,
                calledId = u.calledId,
                callDate = u.callDate,
                callDescription = u.callDescription,
                callerName = u.callerName,
                creationDate = u.creationDate,
                updatedDate = u.updatedDate,
            });
        }

        public string Add(CallAddDTO u)
        {
            Call call = new Call
            {
                callId = Guid.NewGuid().ToString(),
                callerName = u.callerName,
                callDate = u.callDate,
                callDescription = u.callDescription,
                calledId = u.calledId,
                creationDate = DateTime.UtcNow,
                updatedDate = DateTime.UtcNow,
            };
            _callRepo.Add(call);
            _callRepo.SaveChanges();
            return call.callId;
        }

        public bool Delete(string id)
        {
            Call? call = _callRepo.GetById(id);
            if (call == null)
            {
                return false;
            }
            _callRepo.Delete(call);
            _callRepo.SaveChanges();
            return true;
        }

        public CallDTO GetCall(string id)
        {
            Call? call = _callRepo.GetById(id);
            if (call is null)
            {
                return null;
            }

            return new CallDTO
            {
                callId = id,
                callerName = call.callerName,
                callDate = call.callDate,
                callDescription = call.callDescription,
                calledId = call.calledId,
                creationDate = call.creationDate,
                updatedDate = call.updatedDate,
            };
        }
        public Call GetCallById(string id)
        {
            Call? call = _callRepo.GetById(id) as Call;
            if (call == null)
            {
                return null;
            }
            return call;
        }

        public bool Update(CallDTO callRequest)
        {
            if (callRequest.callId == null)
            {
                return false;
            }
            Call? call = _callRepo.GetById(callRequest.callId);
            if (call is null)
            {
                return false;
            }
            call.callerName = callRequest.callerName;
            call.callDate = callRequest.callDate;
            call.callDescription = callRequest.callDescription;
            call.calledId = callRequest.calledId;
            call.updatedDate = DateTime.UtcNow;
            _callRepo.Update(call);
            _callRepo.SaveChanges();
            return true;

        }
    }
}

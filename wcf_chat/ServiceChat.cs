using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace wcf_chat
{
  
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private readonly List<ServerUser> _users = new List<ServerUser>();
        int _nextId = 1;

        public int Connect(string name)
        {
            var user = new ServerUser() {
                Id = _nextId,
                Name = name,
                OperationContext = OperationContext.Current
            };
            _nextId++;
            
            SendMsg(": "+user.Name+" connected to chat!",0);
            _users.Add(user);
            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = _users.FirstOrDefault(i => i.Id == id);
            if (user!=null)
            {
                _users.Remove(user);
                SendMsg(": "+user.Name + " disconnect from chat!",0);
            }
        }

        public void SendMsg(string msg, int id)
        {
            foreach (var item in _users)
            {
                var answer = DateTime.Now.ToShortTimeString();

                var user = _users.FirstOrDefault(i => i.Id == id);
                if (user != null)
                {
                    answer += ": " + user.Name+" ";
                }
                answer += msg;
                item.OperationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
            }
        }

        public string GetMessage()
        {
            return "Test Message";
        }
    }
}

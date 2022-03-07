using System.ServiceModel;
namespace wcf_chat
{
    public class ServerUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public OperationContext OperationContext { get; set; }
    }
}

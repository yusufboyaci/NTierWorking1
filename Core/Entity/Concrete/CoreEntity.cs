using Core.Entity.Abstract;
using Core.Enum;
using Core.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public class CoreEntity : IEntity<Guid>
    {
        NetworkFunctions functions = new NetworkFunctions();
        public CoreEntity()
        {
            this.Status = Status.None;
            this.CreatedDate = DateTime.Now;
            this.CreatedADUserName = WindowsIdentity.GetCurrent().Name;
            this.CreatedComputerName = Environment.MachineName;
            this.CreatedIP = functions.GetLocalIPAddress();
            this.CreatedBy = Environment.UserName;
        }
        public Guid Id { get; set; }
        public Guid? MasterId { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public string CreatedADUserName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }
        public string ModifiedADUserName { get; set; }
        public string ModifiedBy { get; set; }

    }
}

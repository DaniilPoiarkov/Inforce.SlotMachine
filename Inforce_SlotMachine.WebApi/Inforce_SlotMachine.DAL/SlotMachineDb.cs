using Inforce_SlotMachine.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inforce_SlotMachine.DAL
{
    public class SlotMachineDb
    {
        public List<User> Users { get; set; } = new();
    }
}

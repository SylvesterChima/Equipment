using Equipment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Equipment.ViewModels
{
    public class EquipmentsViewModel: BaseViewModel
    {
        public class Nav
        {
            public NetworkAuthData Data { get; set; }
        }
        public override Task Initialize(object initData)
        {
            return base.Initialize(initData);   

        }
    }
}

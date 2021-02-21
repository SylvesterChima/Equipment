using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Equipment.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : FreshBasePageModel
    {
        public string Title { get; set; }
        public async override void Init(object initData)
        {
            base.Init(initData);
            await Initialize(initData);
        }


        public virtual Task Initialize(object initData)
        {
            return Task.CompletedTask;
        }
    }
}

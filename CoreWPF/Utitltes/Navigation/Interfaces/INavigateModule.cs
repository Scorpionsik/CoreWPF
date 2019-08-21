using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWPF.Utitltes.Navigation.Interfaces
{
    public interface INavigateModule
    {
        string Subtitle { get; }
        void OnNavigatingTo(object arg);
        void OnNavigatingFrom();
    }
}

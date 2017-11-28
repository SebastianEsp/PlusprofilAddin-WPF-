using EA;
using System;
using System.Windows;

namespace PlusprofilAddin.ViewModels
{
    abstract class DialogViewModel
    {
        public Repository Repository { get; set; }

        abstract public void Initialize();
    }
}

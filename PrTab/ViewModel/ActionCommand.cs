﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrTab.ViewModel
{
    /**
     * Clase que implementa la interfaz ICommand
     * */
    public class ActionCommand :ICommand
    {
        //Accion que se realiza
        Action action;
        //Constructor
        public ActionCommand(Action accion)
        {
            action = accion;
        }
        //Método que dice si se puede ejecutar o no el comando.
        public bool CanExecute(object parameter)
        {
            return true;
        }
        //El evento que se lanza.
        public event EventHandler CanExecuteChanged;
        //Ejecuta el evento.
        public void Execute(object parameter)
        {
            action();
        }
    }
}

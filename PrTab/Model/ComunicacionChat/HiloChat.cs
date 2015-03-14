using PrTab.ComunicacionChat;
using PrTab.Model.Base_de_Datos;
using PrTab.Model.Modelo.ChatServer;
using PrTab.Utiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.ComunicacionChat
{
    class HiloChat
    {
        private static HiloChat instance = null;

        const string dir = "192.168.0.2";
        const int port = 8000;
        private SocketClient client = new SocketClient();
        private BackgroundWorker bw = new BackgroundWorker();
        List<string> salasConectado = new List<string>();
        private CDB_MensajesServerMessage dataBase = new CDB_MensajesServerMessage();

        protected HiloChat()
        {
            inicializaElHilo();
            conectWithServer();
        }

        public List<string> getRooms()
        {
            return salasConectado;
        }

        public static HiloChat getInstance()
        {
            if(instance == null)
            {
                instance = new HiloChat();
            }
            return instance;
        }

        private void conectWithServer()
        {
            string result = client.Connect(dir, port);
            if (result == "Success")
            {

                //ponerMensaje(new ChatMensaje("El servidor responde", "lalala", "System", 0), false);
                string reg = client.Register();
                if (reg == "Success")
                {
                    //ponerMensaje(new ChatMensaje("Te has conectado al chat!", "lalala", "System", 0), false);
                    salasConectado.Add(AplicationSettings.getIdUniversidadUsuario());
                    bw.RunWorkerAsync();
                }

            }
            //else
                //ponerMensaje(new ChatMensaje("Hay algun problema", "lalala", "System", 0), false);
        }

        public bool sendMessage(MensajeServerMensaje m)
        {
            var ret = client.SendMessage(m);
            return ret == "Success";
        }


        private void inicializaElHilo()
        {
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            //bool getMsg = false;
            while (worker.CancellationPending == false)
            {
                /*string msg = client.Receive();
                ponerMensaje(msg);*/
                //newMsg = client.Receive();
                //if (newMsg != "Operation Timeout")
                //getMsg = true;
                MensajeServer newMsg = client.Receive();
                if (newMsg.type=="status")
                {

                }
                else if (newMsg.type=="message")
                {
                    dataBase.insert((MensajeServerMensaje)newMsg);
                }
                else if (newMsg.type=="error")
                {

                }
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //ponerMensaje(newMsg, false);
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }

    }
}

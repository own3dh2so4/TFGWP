using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrTab.Utiles
{
    class SocketClient
    {

        // Cached Socket object that will be used by each call for the lifetime of this class
        Socket _socket = null;

        // Signaling object used to notify when an asynchronous operation is completed
        static ManualResetEvent _clientDone_con = new ManualResetEvent(false);
        static ManualResetEvent _clientDone_send = new ManualResetEvent(false);
        static ManualResetEvent _clientDone_recive = new ManualResetEvent(false);
        static ManualResetEvent _clientDone_register = new ManualResetEvent(false);

        // Define a timeout in milliseconds for each asynchronous call. If a response is not received within this 
        // timeout period, the call is aborted.
        const int TIMEOUT_MILLISECONDS = 5000;

        // The maximum size of the data buffer to use with the asynchronous socket methods
        const int MAX_BUFFER_SIZE = 2048;


        public string respuesta = "";


        /// <summary>
        /// Attempt a TCP socket connection to the given host over the given port
        /// </summary>
        /// <param name="hostName">The name of the host</param>
        /// <param name="portNumber">The port number to connect</param>
        /// <returns>A string representing the result of this connection attempt</returns>
        public string Connect(string hostName, int portNumber)
        {
            string result = string.Empty;
            char comunication = 'c';

            // Create DnsEndPoint. The hostName and port are passed in to this method.
            DnsEndPoint hostEntry = new DnsEndPoint(hostName, portNumber);

            // Create a stream-based, TCP socket using the InterNetwork Address Family. 
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Create a SocketAsyncEventArgs object to be used in the connection request
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = hostEntry;

            // Inline event handler for the Completed event.
            // Note: This event handler was implemented inline in order to make this method self-contained.
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                if (comunication == 'c')
                {    // Retrieve the result of this request
                    result = e.SocketError.ToString();

                    // Signal that the request is complete, unblocking the UI thread
                    _clientDone_con.Set();
                }
            });

            // Sets the state of the event to nonsignaled, causing threads to block
            _clientDone_con.Reset();

            // Make an asynchronous Connect request over the socket
            _socket.ConnectAsync(socketEventArg);

            // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
            // If no response comes back within this time then proceed
            _clientDone_con.WaitOne(TIMEOUT_MILLISECONDS);

            return result;
        }


        public string Register()
        {
            string response = "Operation Timeout";
            char comunication = 'r';

            // We are re-using the _socket object initialized in the Connect method
            if (_socket != null)
            {
                //Creo los credenciales del registro.
                ChatRegister credenciales = new ChatRegister(AplicationSettings.getToken(), AplicationSettings.getIdUniversidadUsuario());

                //Paso la credencial a un string que tenga el JSON
                string credencialesJSON = JsonConvert.SerializeObject(credenciales);

                // Create SocketAsyncEventArgs context object
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();

                // Set properties on context object
                socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
                socketEventArg.UserToken = null;

                // Inline event handler for the Completed event.
                // Note: This event handler was implemented inline in order 
                // to make this method self-contained.
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    if (comunication == 'r')
                    {
                        response = e.SocketError.ToString();

                        // Unblock the UI thread
                        _clientDone_register.Set();
                    }
                });

                // Add the data to be sent into the buffer
                byte[] payload = Encoding.UTF8.GetBytes(credencialesJSON);
                socketEventArg.SetBuffer(payload, 0, payload.Length);

                // Sets the state of the event to nonsignaled, causing threads to block
                _clientDone_register.Reset();

                // Make an asynchronous Send request over the socket
                _socket.SendAsync(socketEventArg);

                // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
                // If no response comes back within this time then proceed
                _clientDone_register.WaitOne(TIMEOUT_MILLISECONDS);
            }
            else
            {
                response = "Socket is not initialized";
            }

            return response;


        }

        /// <summary>
        /// Send the given data to the server using the established connection
        /// </summary>
        /// <param name="data">The data to send to the server</param>
        /// <returns>The result of the Send request</returns>
        public string Send(string data)
        {
            string response = "Operation Timeout";
            char comunication = 's';

            // We are re-using the _socket object initialized in the Connect method
            if (_socket != null)
            {
                // Create SocketAsyncEventArgs context object
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();

                // Set properties on context object
                socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
                socketEventArg.UserToken = null;

                // Inline event handler for the Completed event.
                // Note: This event handler was implemented inline in order 
                // to make this method self-contained.
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    if (comunication == 's')
                    {
                        response = e.SocketError.ToString();

                        // Unblock the UI thread
                        _clientDone_send.Set();
                    }
                });

                // Add the data to be sent into the buffer
                byte[] payload = Encoding.UTF8.GetBytes(data);
                socketEventArg.SetBuffer(payload, 0, payload.Length);

                // Sets the state of the event to nonsignaled, causing threads to block
                _clientDone_send.Reset();

                // Make an asynchronous Send request over the socket
                _socket.SendAsync(socketEventArg);

                // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
                // If no response comes back within this time then proceed
                _clientDone_send.WaitOne(TIMEOUT_MILLISECONDS);
            }
            else
            {
                response = "Socket is not initialized";
            }

            return response;
        }


        public string SendMessage(ChatMensaje data)
        {
            string response = "Operation Timeout";
            char comunication = 's';

            // We are re-using the _socket object initialized in the Connect method
            if (_socket != null)
            {
                // Create SocketAsyncEventArgs context object
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();

                // Set properties on context object
                socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
                socketEventArg.UserToken = null;

                // Inline event handler for the Completed event.
                // Note: This event handler was implemented inline in order 
                // to make this method self-contained.
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    if (comunication == 's')
                    {
                        response = e.SocketError.ToString();

                        // Unblock the UI thread
                        _clientDone_send.Set();
                    }
                });

                //Transformo el ChatMessage a JSON
                string dataJSON = JsonConvert.SerializeObject(data);

                // Add the data to be sent into the buffer
                byte[] payload = Encoding.UTF8.GetBytes(dataJSON);
                socketEventArg.SetBuffer(payload, 0, payload.Length);

                // Sets the state of the event to nonsignaled, causing threads to block
                _clientDone_send.Reset();

                // Make an asynchronous Send request over the socket
                _socket.SendAsync(socketEventArg);

                // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
                // If no response comes back within this time then proceed
                _clientDone_send.WaitOne(TIMEOUT_MILLISECONDS);
            }
            else
            {
                response = "Socket is not initialized";
            }

            return response;
        }

        /// <summary>
        /// Receive data from the server using the established socket connection
        /// </summary>
        /// <returns>The data received from the server</returns>
        public ChatMensaje Receive()
        {
            //string response = "Operation Timeout";
            char comunication = 'r';

            //Creamos el mensaje que nos ha mandado el servidor
            ChatMensaje msg = new ChatMensaje("Operation time out!","lala","SystemError",0);

            // We are receiving over an established socket connection
            if (_socket != null)
            {
                // Create SocketAsyncEventArgs context object
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;

                // Setup the buffer to receive the data
                socketEventArg.SetBuffer(new Byte[MAX_BUFFER_SIZE], 0, MAX_BUFFER_SIZE);

                // Inline event handler for the Completed event.
                // Note: This even handler was implemented inline in order to make 
                // this method self-contained.
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    if (comunication == 'r')
                    {
                        if (e.SocketError == SocketError.Success)
                        {
                            // Retrieve the data from the buffer
                            respuesta = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                            respuesta = respuesta.Trim('\0');
                            //TODO: CONTINUAR AQUI.
                            JObject o = JObject.Parse(respuesta);
                            if((string)o.SelectToken("error") == "201")
                            {
                                msg = new ChatMensaje((string)o.SelectToken("message"),
                                                    (string)o.SelectToken("room"),
                                                    (string)o.SelectToken("name"),
                                                    Convert.ToInt32((string)o.SelectToken("user_id")));
                            }
                            else if ((string)o.SelectToken("error") == "200")
                            {
                                msg = new ChatMensaje("Tu estado ahora es: "+(string)o.SelectToken("state"),
                                                    "room",
                                                    "server",
                                                    0);
                            }

                        }
                        else
                        {
                            //respuesta = e.SocketError.ToString();
                            msg = new ChatMensaje(e.SocketError.ToString(),
                                                    "room",
                                                    "SystemRecivFail",
                                                    0);
                        }

                        _clientDone_recive.Set();
                    }
                });

                // Sets the state of the event to nonsignaled, causing threads to block
                _clientDone_recive.Reset();

                // Make an asynchronous Receive request over the socket
                _socket.ReceiveAsync(socketEventArg);

                // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
                // If no response comes back within this time then proceed
                //_clientDone.WaitOne(TIMEOUT_MILLISECONDS);
                _clientDone_recive.WaitOne();
            }
            else
            {
                //respuesta = "Socket is not initialized";
                msg = new ChatMensaje("Socket is not initialized",
                                      "room",
                                      "SystemRecivFail",
                                      0);
            }

            //return respuesta;
            return msg;
        }

        /// <summary>
        /// Closes the Socket connection and releases all associated resources
        /// </summary>
        public void Close()
        {
            if (_socket != null)
            {
                _socket.Close();
            }
        }





    }
}

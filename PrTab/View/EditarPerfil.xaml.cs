﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Threading.Tasks;
using PrTab.Model.Comunicacion;
using PrTab.Utiles;

namespace PrTab.View
{
    public partial class EditarPerfil : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;
        string fileName = "perfil.jpg";
        string folderName = "fotos";

        public EditarPerfil()
        {
            InitializeComponent();
            this.photoChooserTask = new PhotoChooserTask();
            this.photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);

            this.Loaded += Inicilizar;
        }

        private void Inicilizar(object sender, RoutedEventArgs e)
        {
            ponerFoto();            
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            BitmapImage image = new BitmapImage();
            saveImage(e);            
        }

        private async void saveImage(PhotoResult e)
        {
            // photo selected
            if (e.ChosenPhoto != null)
            {
                    // get the file stream and file name
                    Stream photoStream = e.ChosenPhoto;                 

                    StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                    var dataFolder = await local.CreateFolderAsync(folderName,CreationCollisionOption.OpenIfExists);

                    StorageFile myfile = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                    using (Stream stream = await myfile.OpenStreamForWriteAsync())
                    {
                        
                        byte[] b = ReadFully(photoStream);
                        stream.Write(b, 0, b.Length);
                    } 
                ponerFoto();
                sendImagePerfilServer();
            }
        }


        private void BotonCambiarImagen_Click(object sender, RoutedEventArgs e)
        {
            photoChooserTask.Show(); 
        }


        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private async void ponerFoto ()
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            var dataFolder = await local.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            // Get the file.
            var file = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            using (Stream stream = await file.OpenStreamForWriteAsync())
            {
                if (stream.Length != 0)
                {
                    BitmapImage a = new BitmapImage();
                    a.SetSource(stream);
                    imagenPerfil.Source = a;
                }
            }
        }

        private async void sendImagePerfilServer()
        {
            //StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var dataFolder = await local.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            //// Get the file.
            //var file = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            //using (Stream stream = await file.OpenStreamForReadAsync())
            //{
            //    if (stream.Length>0)
            //        Comunicacion.sendImagePerfil(AplicationSettings.getToken(),stream);
            //}
            //await Comunicacion.sendImagePerfil(AplicationSettings.getToken(), folderName, fileName);


            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            var dataFolder = await local.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            // Get the file.
            var file = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            using (Stream stream = await file.OpenStreamForReadAsync())
            {
                if (stream.Length > 0)
                {
                    byte[] bytes = ReadFully(stream);
                    Comunicacion.sendImagePerfil(AplicationSettings.getToken(), bytes);
                }
                    
            }
        }

    }
}
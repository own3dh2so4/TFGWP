using System;
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
            this.photoChooserTask.PixelHeight = 300;
            this.photoChooserTask.PixelWidth = 300;
            this.photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);

            this.Loaded += Inicilizar;
        }

        private void Inicilizar(object sender, RoutedEventArgs e)
        {
            ponerFoto();            
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            
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
                    byte[] b;

                    using (Stream stream = await myfile.OpenStreamForWriteAsync())
                    {
                        
                        b = ReadFully(photoStream);
                        stream.Write(b, 0, b.Length);
                        stream.Close();
                    } 
                await ponerFoto();
                if (b.Length>0)
                    await sendImagePerfilServer(b);
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

        private async Task<bool> ponerFoto ()
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
                    return true;
                }
                stream.Close();
            }
            return false;
        }

        private async Task<bool> sendImagePerfilServer( byte[] b)
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


            //StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var dataFolder = await local.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            //// Get the file.
            //var file = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            //using (Stream stream = await file.OpenStreamForReadAsync())
            //{
            //    if (stream.Length > 0)
            //    {
            //        byte[] bytes = ReadFully(stream);
            //        Comunicacion.sendImagePerfil(AplicationSettings.getToken(), bytes);
            //        return true;
            //    }
                    
            //}
            //return false;

            if (b.Length>0)
            {
                Comunicacion.sendImagePerfil(AplicationSettings.getToken(), b);
                return true;
            }
            return false;
        }

    }
}
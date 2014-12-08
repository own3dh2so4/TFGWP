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
using PrTab.Model.Modelo;

namespace PrTab.View
{
    public partial class EditarPerfil : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;
        string fileName = "perfil.jpg";
        string folderName = "fotos";


        private List<Provincia> provincias = new List<Provincia>();
        private List<Universidad> universidades = new List<Universidad>();
        private List<Facultad> facultades = new List<Facultad>();

        private int id_provincia = 0;
        private int id_universidad = 0;
        private int id_facultad = 0;

        public EditarPerfil()
        {
            InitializeComponent();
            this.photoChooserTask = new PhotoChooserTask();
            this.photoChooserTask.PixelHeight = 300;
            this.photoChooserTask.PixelWidth = 300;
            this.photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);

            this.Loaded += Inicilizar;
        }

        private async void Inicilizar(object sender, RoutedEventArgs e)
        {
            ponerFoto();
            ListItemProvincias.Items.Add("");
            provincias = await Comunicacion.getProvicias();
            foreach (var provincia in provincias)
            {
                ListItemProvincias.Items.Add(provincia.nombre);
            }
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
            if (b.Length>0)
            {
                Comunicacion.sendImagePerfil(AplicationSettings.getToken(), b);
                return true;
            }
            return false;
        }

        private void ListPicker_ProvicniaSeleccionada(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListPicker_UniversidadSeleccionada(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListPicker_FacultadSeleccionada(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
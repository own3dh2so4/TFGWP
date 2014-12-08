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
using Windows.Web.Http;
using Newtonsoft.Json.Linq;

namespace PrTab.View
{
    public partial class EditarPerfil : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;
        string fileName = "perfil.jpg";
        string folderName = "fotos";


        //private List<Provincia> provincias = new List<Provincia>();
        //private List<Universidad> universidades = new List<Universidad>();
        //private List<Facultad> facultades = new List<Facultad>();

        //private int id_provincia = 0;
        //private int id_universidad = 0;
        //private int id_facultad = 0;

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
            
            //ListItemProvincias.Items.Add("");
            //provincias = await Comunicacion.getProvicias();
            //foreach (var provincia in provincias)
            //{
            //    ListItemProvincias.Items.Add(provincia.nombre);
            //}
            await ponerFoto();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/EditarPerfilDatosPersonales.xaml", UriKind.Relative));
        }

        //private async void ListPicker_ProvicniaSeleccionada(object sender, SelectionChangedEventArgs e)
        //{
        //    List<string> unis = new List<string>();
        //        unis.Add("");                
        //        universidades = await Comunicacion.getUniversidadesProvincia(elementoSelecionadoProvincia(ListItemProvincias.SelectedItem.ToString()));
        //        foreach(var uni in universidades)
        //        {
        //            unis.Add(uni.nombre);
        //        }
        //        ListItemUniversidad.ItemsSource = unis;
        //        ListItemUniversidad.IsEnabled = true;
            
        //}

        //private async void ListPicker_UniversidadSeleccionada(object sender, SelectionChangedEventArgs e)
        //{
        //    if (ListItemUniversidad.SelectedItem != null && ListItemUniversidad.SelectedItem.ToString() != "")
        //    {
        //        List<string> faculs = new List<string>();
        //        faculs.Add("");
        //        facultades = await Comunicacion.getFacultadesUniversidad(elementoSelecionadoUniversidad(ListItemUniversidad.SelectedItem.ToString()));
        //        foreach (var fac in facultades)
        //        {
        //            faculs.Add(fac.nombre);
        //        }
        //        ListItemFacultades.ItemsSource = faculs;
        //        ListItemFacultades.IsEnabled = true;
        //    }
        //}

        //private void ListPicker_FacultadSeleccionada(object sender, SelectionChangedEventArgs e)
        //{
        //    if (ListItemFacultades.SelectedItem != null && ListItemFacultades.SelectedItem.ToString() != "")
        //    {
        //        elementoSelecionadoFacultad(ListItemFacultades.SelectedItem.ToString());
        //    }
        //}

        //private int elementoSelecionadoProvincia(String name)
        //{
        //    int i = 0;
        //    foreach (var provincia in provincias)
        //    {
        //        if(provincia.nombre==name)
        //        {
        //            id_provincia = provincia.identificador;
        //            return provincia.identificador;
        //        }
        //        i++;
        //    }
        //    return -1;
        //}

        //private int elementoSelecionadoUniversidad(String name)
        //{
        //    int i = 0;
        //    foreach (var uni in universidades)
        //    {
        //        if (uni.nombre == name)
        //        {
        //            id_universidad = uni.identificador;
        //            return uni.identificador;
        //        }
        //        i++;
        //    }
        //    return -1;
        //}

        //private int elementoSelecionadoFacultad(String name)
        //{
        //    int i = 0;
        //    foreach (var fac in facultades)
        //    {
        //        if (fac.nombre == name)
        //        {
        //            id_facultad = fac.identificador;
        //            return fac.identificador;
        //        }
        //        i++;
        //    }
        //    return -1;
        //}

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (AntiguaContraseña.Password != "" && AntiguaContraseña.Password != null)
        //    {
        //        if (NuevaContraseña.Password != "" && NuevaContraseña.Password  != null)
        //        {
        //            if (NuevaContraseña.Password.Length < 6)
        //            {
        //                MessageBox.Show("Las contraseñas tienen que tener 6 caracteres minimo.");
        //            }
        //            else
        //            {
        //                var respuesta = await Comunicacion_Usuario.CambiarContraseña(AntiguaContraseña.Password, NuevaContraseña.Password);
        //                if (!respuesta)
        //                {
        //                    MessageBox.Show(AplicationSettings.getErrorServer());
        //                }
        //            }
        //        }
        //        if (id_facultad!=0)
        //        {
        //            var respuesta = await Comunicacion_Usuario.CambiarFacultad(id_facultad + "", AntiguaContraseña.Password);
        //            if (!respuesta)
        //            {
        //                MessageBox.Show(AplicationSettings.getErrorServer());
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Introduce tu contraseña actual.");
        //    }
            
        //}

    }
}
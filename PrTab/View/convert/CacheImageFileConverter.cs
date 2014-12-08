using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Media.Imaging;
using Windows.Networking.Connectivity;
using System.Threading.Tasks;
using PrTab.Model.Modelo;
using PrTab.Model.Base_de_Datos;


namespace PrTab.View.convert
{
    public class CacheImageFileConverter : IValueConverter
    {
        
        private static IsolatedStorageFile _storage = IsolatedStorageFile.GetUserStoreForApplication();
        private const string imageStorageFolder = "TempImages";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = value as string;
            if (String.IsNullOrEmpty(path)) return null;
            string unixTime = "tiempo";
            string unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds+"";
            Uri imageFileUri = new Uri(path + "?" + unixTime + "=" + unixTimestamp);
            ConnectionProfile InternetConnectionProfile;
            InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
            if (imageFileUri.Scheme == "http" || imageFileUri.Scheme == "https")
            {
                if (InternetConnectionProfile == null)
                {
                    if (_storage.FileExists(GetFileNameInIsolatedStorage(imageFileUri)))
                    {
                        return ExtractFromLocalStorage(imageFileUri);
                    }
                    else
                    {
                        //return LoadDefaultIfPassed(imageFileUri, (parameter ?? string.Empty).ToString());
                        return LoadDefaultIfPassed(imageFileUri, "/View/fotos/no_image.png");
                    }
                }
                else
                {
                    return DownloadFromWeb(imageFileUri);
                }
            }
            else
            {
                BitmapImage bm = new BitmapImage(imageFileUri);
                return bm;
            }
        }

        private static object LoadDefaultIfPassed(Uri imageFileUri, string defaultImagePath)
        {
            string defaultImageUri = (defaultImagePath ?? string.Empty).ToString();
            if (!string.IsNullOrEmpty(defaultImageUri))
            {
                BitmapImage bm = new BitmapImage(new Uri(defaultImageUri, UriKind.Relative));         //Load default Image
                return bm;
            }
            else
            {
                BitmapImage bm = new BitmapImage(imageFileUri);
                return bm;
            }
        }

        private static object DownloadFromWeb(Uri imageFileUri)
        {
                                          //Load from internet
            BitmapImage bm = new BitmapImage();
            CarrgarFromServer carga = new CarrgarFromServer();
            carga.fotoCargada += (s, a) =>
            {
                if (!a.fromServer)
                {
                    string isolatedStoragePath = GetFileNameInIsolatedStorage(imageFileUri);       //Load from local storage
                    using (var sourceFile = _storage.OpenFile(isolatedStoragePath, FileMode.Open, FileAccess.Read))
                    {                       
                        bm.SetSource(sourceFile);
                    }
                }
                else
                {
                    WebClient m_webClient = new WebClient();
                    m_webClient.OpenReadCompleted += (o, e) =>
                    {
                        if (e.Error != null || e.Cancelled) return;
                        CacheImageFileConverter.WriteToIsolatedStorage(IsolatedStorageFile.GetUserStoreForApplication(), e.Result, CacheImageFileConverter.GetFileNameInIsolatedStorage(imageFileUri));
                        bm.SetSource(e.Result);
                        e.Result.Close();
                    };
                    m_webClient.OpenReadAsync(imageFileUri);

                }
            };

            carga.getImageFromServer(imageFileUri);

           
            return bm;
        }

        

        public static object ExtractFromLocalStorage(Uri imageFileUri)
        {
            string isolatedStoragePath = GetFileNameInIsolatedStorage(imageFileUri);       //Load from local storage
            using (var sourceFile = _storage.OpenFile(isolatedStoragePath, FileMode.Open, FileAccess.Read))
            {
                BitmapImage bm = new BitmapImage();
                bm.SetSource(sourceFile);
                return bm;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static void WriteToIsolatedStorage(IsolatedStorageFile storage, System.IO.Stream inputStream, string fileName)
        {
            IsolatedStorageFileStream outputStream = null;
            try
            {
                if (!storage.DirectoryExists(imageStorageFolder))
                {
                    storage.CreateDirectory(imageStorageFolder);
                }
                if (storage.FileExists(fileName))
                {
                    storage.DeleteFile(fileName);
                }
                outputStream = storage.CreateFile(fileName);
                byte[] buffer = new byte[inputStream.Length];
                int read;
                while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, read);
                }
                outputStream.Close();
            }
            catch
            {
                //We cannot do anything here.
                if (outputStream != null) outputStream.Close();
            }
        }

        /// <summary>
        /// Gets the file name in isolated storage for the Uri specified. This name should be used to search in the isolated storage.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static string GetFileNameInIsolatedStorage(Uri uri)
        {
            return imageStorageFolder + "\\" + uri.AbsoluteUri.GetHashCode() + ".png";
        }

    }

    class CarrgarFromServer
    {
        public EventHandler<FotoCargardaEventArgs> fotoCargada;
        private static CDB_CacheImagen bdCache = new CDB_CacheImagen();

        public async void getImageFromServer(Uri imageFileUri)
        {
            //BitmapImage bm = new BitmapImage();
            //WebClient m_webClient = new WebClient();
            //m_webClient.OpenReadCompleted += (o, e) =>
            //{
            //    if (e.Error != null || e.Cancelled) return;
            //    CacheImageFileConverter.WriteToIsolatedStorage(IsolatedStorageFile.GetUserStoreForApplication(), e.Result, CacheImageFileConverter.GetFileNameInIsolatedStorage(imageFileUri));
            //    bm.SetSource(e.Result);
            //    e.Result.Close();
            //};
            bool fs = false;

            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(imageFileUri);
            webRequest.Method = "HEAD";
            HttpWebResponse webResponse = ((HttpWebResponse)await webRequest.GetResponseAsync());
            var fechaUltimaMod = webResponse.Headers["Last-Modified"];
            CacheImagen cache = new CacheImagen(imageFileUri.ToString(), fechaUltimaMod.ToString());
            CacheImagen accesBDCache = bdCache.getCacheImage(cache.url);
            if (accesBDCache != null)
            {
                if (cache.date == accesBDCache.date)
                {
                    //bm = (BitmapImage)CacheImageFileConverter.ExtractFromLocalStorage(imageFileUri);
                    fs = false;
                }
                else
                {
                    //bdCache.insert(cache);
                    //m_webClient.OpenReadAsync(imageFileUri);
                    fs = true;
                }
            }
            else
            {
                //bdCache.insert(cache);
                //m_webClient.OpenReadAsync(imageFileUri);
                fs = true;
            }
            webResponse.Close();
            if (fotoCargada!=null)
            {
                fotoCargada(this, new FotoCargardaEventArgs(fs));
            }
        }
    }

    class FotoCargardaEventArgs : EventArgs
    {
        //public BitmapImage bitmap { get; set; }
        public bool fromServer;

        public FotoCargardaEventArgs( bool s)
        {
            //bitmap = a;
            fromServer = s;
        }
    }
}

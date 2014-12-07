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
        private static CDB_CacheImagen bdCache = new CDB_CacheImagen();
        private static IsolatedStorageFile _storage = IsolatedStorageFile.GetUserStoreForApplication();
        private const string imageStorageFolder = "TempImages";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = value as string;
            if (String.IsNullOrEmpty(path)) return null;
            Uri imageFileUri = new Uri(path);
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

        private async static Task<object> DownloadFromWeb(Uri imageFileUri)
        {
            WebClient m_webClient = new WebClient();                                //Load from internet
            BitmapImage bm = new BitmapImage();

            m_webClient.OpenReadCompleted += (o, e) =>
            {
                if (e.Error != null || e.Cancelled) return;
                WriteToIsolatedStorage(IsolatedStorageFile.GetUserStoreForApplication(), e.Result, GetFileNameInIsolatedStorage(imageFileUri));
                bm.SetSource(e.Result);
                e.Result.Close();
            };

            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(imageFileUri);
            webRequest.Method="HEAD";
            HttpWebResponse webResponse = ((HttpWebResponse)await webRequest.GetResponseAsync());
            var fechaUltimaMod = webResponse.Headers["Last-Modified"];
            CacheImagen cache = new CacheImagen(imageFileUri.ToString(), fechaUltimaMod.ToString());
            CacheImagen accesBDCache = bdCache.getCacheImage(cache.url);
            if (accesBDCache!=null)
            {
                if (cache.date==accesBDCache.date)
                {
                    return ExtractFromLocalStorage(imageFileUri);
                }
                else
                {
                    bdCache.insert(cache);
                    m_webClient.OpenReadAsync(imageFileUri);
                }
            }
            else
            {
                bdCache.insert(cache);
                m_webClient.OpenReadAsync(imageFileUri);
            }
            webResponse.Close();
            return bm;
        }

        private static object ExtractFromLocalStorage(Uri imageFileUri)
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

        private static void WriteToIsolatedStorage(IsolatedStorageFile storage, System.IO.Stream inputStream, string fileName)
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
                byte[] buffer = new byte[32768];
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
}

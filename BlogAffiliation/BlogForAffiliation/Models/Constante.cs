using Sxylo_Stock.Model.Entities;
using Sxylo_Stock.Model;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Sxylo_Stock.Database
{
    public static class Constante

    {
        private static readonly Sxylo_Stock.Model.DatabaseContext databaseContext;
        public static string formatDate = "dd-MM-yyyy";

        public static TimeSpan ts = new TimeSpan(10, 30, 0);

        public static String urlApiCourriel = "https://sendmail.niovar.ca";

        public static String urlApiSaveFile = "https://fichiers.niovarpaie.ca/";


        // cette fonction va permettre la generation du code de verifcation de l'utilisateur
        public static string GenerateCodeVerification()
        {
            string numbers = "1234567890";
            Random objrandom = new Random();
            string strRandom = string.Empty;

            for (int i = 0; i < 4; i++)
            {
                int temp = objrandom.Next(0, numbers.Length);
                strRandom += temp;
            }
            return strRandom;
        }

        // Recuperer les images d'un article
        public static Dictionary<string, Object> getImageArticle(PictureArticle item)
        {
            var json = new Dictionary<string, Object>();
            if (item == null) return null;
            json.Add("Image", item.pathJoinced);
           
            return (json);
        }

        // cette focntion va permettre de recuperer les articles avec leurs images
        public static Dictionary<string, Object> getJsonAll(Article item)
        {
            var json = new Dictionary<string, Object>();
            if (item == null) return null;
            json.Add("id", item.id);
            //json.Add("Image", item.pathJoinced);
            json.Add("nameArticle", item.name);
            json.Add("oldPrice", item.oldPrice);
            json.Add("newPrice", item.newPrice);
            json.Add("numSeri", item.numSeri);
            json.Add("codeArticle", item.codeArticle);
            json.Add("quantity", item.quantity);
            json.Add("alertquantity", item.alertquantity);
            json.Add("nbreMonthGaranti", item.nbreMonthGaranti);
            json.Add("dateExpiration", item.dateExpiration);
            json.Add("creatAt", item.creatAt);


            return (json);
        }

        public static Dictionary<string, Object> getJsonAllBySubCategory(Article item)
        {
            var json = new Dictionary<string, Object>();
            if (item == null) return null;
            json.Add("id", item.id);
            json.Add("nameArticle", item.name) ;
            json.Add("oldPrice",item.oldPrice);
            json.Add("newPrice", item.newPrice );
            json.Add("numSeri",  item.numSeri );
            json.Add("codeArticle",  item.codeArticle);
            json.Add("quantity",item.quantity );
            json.Add("alertquantity",item.alertquantity );
            json.Add("nbreMonthGaranti", item.nbreMonthGaranti );
            json.Add("dateExpiration",  item.dateExpiration );
            json.Add("creatAt",  item.creatAt );


            return (json);
        }
       /* public static Dictionary<string, Object> getJsonPictureArticle (PictureArticle item)
        {
            var json = new Dictionary<string, Object>();
            if (item == null) return null; 
            json.Add("id", item.id);
            json.Add("Image", item.pathJoinced); 
            return (json);
        }*/
        public static Dictionary<string, Object> getJsonColorArticle(AssocColorArticle item)
        {
            var json = new Dictionary<string, Object>();
            if (item == null) return null;
            json.Add("id", item.id);
            json.Add("description", item.colorArticle!= null ? item.colorArticle.description : "" );
            return (json);
        }
       
        // cette fonction va permettre le hachage du mot de passe
        public static String hashPassword(String password)
        {
            Encoding enc = Encoding.UTF8;
            Byte[] rawMessage = enc.GetBytes(password);

            SHA1 sha1 = new SHA1CryptoServiceProvider();

            Byte[] result = sha1.ComputeHash(rawMessage);
            string mac = Convert.ToBase64String(result);

            //split the string in 2 and return the first half
            mac = mac.Substring(0, mac.Length / 2);

            return mac;
        }

        public static String GetBaseUrl(HttpRequest request)
        {
            var uriBuilder = new UriBuilder(request.Scheme, request.Host.Host, request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }

            var baseUri = uriBuilder.Uri.AbsoluteUri;
            return baseUri;

        }
        public static string GenerationPassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[10];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }


       

        
        public static String GenerationFileNameArticle()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[10];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public static string GenerationCodeArticle()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[9];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }

    
}


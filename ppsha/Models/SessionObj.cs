using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ppsha.Helper
{
    public class SessionObj
    {
        private static ISession _Session;

        public static void Init(ISession session)
        {
            _Session = session;
        }

        public static string SessionId { get { return _Session.Id; } }

        public static int UserId { get { return Get<int>("UserId"); } set { Set("UserId", value); } }

        public static string Username { get { return Get<string>("Username"); } set { Set("Username", value); } }

        public static string TokenString { get { return Get<string>("TokenString"); } set { Set("TokenString", value); } }

        public static byte[] ClaimPdfBytes { get { return Get<byte[]>("ClaimPdfBytes"); } set { Set("ClaimPdfBytes", value); } }

        public static string ClaimPdfName { get { return Get<string>("ClaimPdfName"); } set { Set("ClaimPdfName", value); } }

        public static double ClaimPdfSize { get { return Get<double>("ClaimPdfSize"); } set { Set("ClaimPdfSize", value); } }

        public static string ClaimStructuredJson { get { return Get<string>("ClaimStructuredJson"); } set { Set("ClaimStructuredJson", value); } }

        public static string ContactId { get { return Get<string>("ContactId"); } set { Set("ContactId", value); } }

        public static string ContactJson { get { return Get<string>("ContactJson"); } set { Set("ContactJson", value); } }

        public static string Environment { get { return Get<string>("Env"); } set { Set("Env", value); } }

        public static List<PageFields> PageFieldList { get { return Get<List<PageFields>>("PageFieldList"); } set { Set("PageFieldList", value); } }




        public static void Remove(string key)
        {
            _Session.Remove(key);
        }

        public static void Clear()
        {
            _Session.Clear();
        }

        // Convert an object to a byte array
        private static void Set<T>(string key, T obj)
        {
            if (obj == null)
            {
                _Session.Set(key, null);
                return;
            }
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            byte[] arrBytes = ms.ToArray();

            _Session.Set(key, arrBytes);
        }

        // Convert a byte array to an Object
        private static T Get<T>(string key)
        {
            try
            {
                if (_Session == null || !_Session.Keys.Contains(key))
                {
                    throw new System.Exception("Invalid Session key");
                }
                byte[] arrBytes = _Session.Get(key);
                MemoryStream memStream = new MemoryStream();
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                T obj = (T)binForm.Deserialize(memStream);
                return obj;
            }
            catch (System.Exception) { throw; }
        }


    }
}

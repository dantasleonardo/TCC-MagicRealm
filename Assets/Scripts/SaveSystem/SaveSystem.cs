using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

#pragma warning disable 168

namespace SaveSystem
{
    public class SaveSystem
    {
        private static readonly string SavePath = $"{Application.persistentDataPath}/user.mr";

        public static void Save(User user)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(SavePath, FileMode.Create);
            
            formatter.Serialize(stream, user);
            stream.Close();
        }

        public static User Load()
        {
            if (!File.Exists(SavePath)) return null;
            var formatter = new BinaryFormatter();
            var stream = new FileStream(SavePath, FileMode.Open);
            User user = null;
            try
            {
                user = formatter.Deserialize(stream) as User;
                stream.Close();
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return user;
        }
    }
}

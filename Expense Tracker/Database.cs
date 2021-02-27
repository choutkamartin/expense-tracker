using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Expense_Tracker
{
    public static class Database
    {

        public static BindingList<Model.User> Users { get; set; } = new BindingList<Model.User>();

        public static class Session // Class for storing session
        {
            public static string Username;
        }

        public static void Init()
        {
            CreateDatabaseFile();
        }

        private static void CreateDatabaseFile() // Create a .xml file if the file doesn't exist
        {
            if (!File.Exists("users.xml"))
            {
                Serialize(Users, "users.xml");
            }
        }

        public static void Serialize<T>(BindingList<T> list, string file)
        {

            using (Stream s = File.Open(file, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BindingList<T>));
                XmlWriter writer = new XmlTextWriter(s, Encoding.UTF8);
                serializer.Serialize(writer, list);
            }
        }

        public static void Deserialize()
        {
            Users = Desearilize<Model.User>("users.xml");
        }

        private static BindingList<T> Desearilize<T>(string file)
        {

            using (Stream s = File.Open(file, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BindingList<T>));
                return (BindingList<T>)serializer.Deserialize(s);
            }
        }
    }
}

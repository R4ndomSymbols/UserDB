using System.Security.Cryptography;
using System.Text;

namespace UserDB.Models.Entities
{
    public class UserDB
    {
        private static string _path = @"‪E:\Data\users.txt";
        private static int _currentId;
        private static SHA256 hashAlg = SHA256.Create();


        public static void WriteToFile(UserViewModel model)
        {
            _currentId++;
            model.Id = _currentId;
            using (StreamWriter sw = new StreamWriter(Path.GetFullPath(_path)))
            {
                sw.WriteLine(ModelToString(model));
            }
        }

        public static UserViewModel CreateFromFile(int id)
        {
            string target = string.Empty;
            using(StreamReader stream = new StreamReader(Path.GetFullPath(_path)))
            {
                try
                {
                    do
                    {
                        target = stream.ReadLine();
                    }
                    while (!stream.EndOfStream && !target.Contains("id: "+ id.ToString()));
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }

            }
            var data = target.Split(' ');
            return new UserViewModel(Convert.ToInt32(data[1]), data[2], data[3]);

        }



        private static string ModelToString(UserViewModel user)
        {
            var hash = hashAlg.ComputeHash(Encoding.Unicode.GetBytes(user.Password));
            return "id: " + user.Id + " " + user.Name + " " + string.Join("", hash.Select(x => $"{x : X2}"));
            
        } 



    }
}

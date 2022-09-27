using System.Security.Cryptography;
using System.Text;

namespace UserDB.Models.Entities
{
    public class UserDB
    {
        private static string _path = @"Data\users.txt";
        private static string _pathToConfig = @"Data\config.txt";
        private static int _currentId;
        private static SHA256 hashAlg = SHA256.Create();

        static UserDB()
        {
            Directory.SetCurrentDirectory(@"E:\");
            _currentId = Convert.ToInt32(File.ReadAllLines(_pathToConfig)[0]);
        }

        public static void WriteToFile(UserViewModel model)
        {
            _currentId++;

            File.WriteAllText(_pathToConfig, _currentId.ToString());

            model.Id = _currentId;
            using (FileStream fs = new FileStream(_path, FileMode.Open))
            {
                fs.Position = fs.Length;
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(ModelToString(model));
                }
            }
        }

        public static UserViewModel CreateFromFile(int id)
        {

            var lines = File.ReadAllLines(_path);
            var targetLine = lines.Where(line => line.Contains("id: " + id.ToString()));

            if (targetLine.Count() == 0)
            {
                return null;

            }
            else
            {
                var t = targetLine.ToArray()[0].Split(" ");
                return new UserViewModel(Convert.ToInt32(t[1]), t[2], t[3]);
            }

        }

        public static void OverrideUser(UserViewModel us)
        {
            var lines = File.ReadAllLines(_path);
            var targetLineChanged = lines.Select(line => line.Contains("id: " + us.Id.ToString())
            ? ModelToString(us) : line);
            File.WriteAllLines(_path, targetLineChanged);
        }

        public static void DeleteUser(int id)
        {
            var lines = File.ReadAllLines(_path);
            var ltargetLineExcluded = lines.Where(line => !line.Contains("id: " + id.ToString()));
            File.WriteAllLines(_path, ltargetLineExcluded);
        }

        private static string ModelToString(UserViewModel user)
        {
            var hash = hashAlg.ComputeHash(Encoding.Unicode.GetBytes(user.Password));
            return "id: " + user.Id + " " + user.Name + " " + string.Join("", hash.Select(x => $"{x:X2}"));
            
        } 
    }
}

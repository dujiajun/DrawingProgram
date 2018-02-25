using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Drawing
{
    class DrawSet
    {
        private List<string> set;
        private Random random;
        private string file;
        public DrawSet(string filename)
        {
            set = new List<string>();
            random = new Random(GetRandomSeed());
            file = filename;
            FileStream fs = new FileStream(@filename, FileMode.OpenOrCreate);

            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            string line = sr.ReadLine();
            while (line != null)
            {
                set.Add(line);
                line = sr.ReadLine();
            }

        }
        public void Reset(string filename)
        {
            set.Clear();
            file = filename;
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);

            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            string line = sr.ReadLine();
            while (line != null)
            {
                set.Add(line);
                line = sr.ReadLine();
            }
        }
        public string DrawOnce()
        {
            return set[random.Next() % set.Count()];
        }
        public string[] DrawTimes(int n)
        {
            List<string> tmp = new List<string>(set);
            string[] ans = new string[n];
            for (int i = 0; i < n; i++)
            {
                string once = tmp[random.Next() % set.Count()];
                tmp.Remove(once);
                ans[i] = once;
            }
            return ans;
        }
        public string[] Upset()
        {
            return DrawTimes(set.Count());
        }
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        public int Count()
        {
            return set.Count();
        }
        public string[] GetAllValues()
        {
            string[] tmp = new string[set.Count()];
            set.CopyTo(tmp);
            return tmp;
        }
        public string GetCurrentFileName()
        {
            return file;
        }
        public void AddItem(string item)
        {
            set.Add(item);
        }
    }
}

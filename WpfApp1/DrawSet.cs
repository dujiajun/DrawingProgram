using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Drawing
{
    class DrawSet
    {
        private List<String> set;
        private Random random;
        private String file;
        public DrawSet(String filename)
        {
            set = new List<string>();
            random = new Random(GetRandomSeed());
            file = filename;
            FileStream fs = new FileStream(@filename,FileMode.OpenOrCreate);
            
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            String line =sr.ReadLine();
            while (line!=null)
            {
                set.Add(line);
                line = sr.ReadLine();
            }
            
        }
        public void Reset(String filename)
        {
            set.Clear();
            file = filename;
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);

            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            String line = sr.ReadLine();
            while (line != null)
            {
                set.Add(line);
                line = sr.ReadLine();
            }
        }
        public String DrawOnce()
        {
            return set[random.Next() % set.Count()];
        }
        public String[] DrawTimes(int n)
        {
            List<String> tmp = new List<String>(set);
            String[] ans = new String[n];
            for(int i=0;i<n;i++)
            {
                String once = tmp[random.Next() % set.Count()];
                tmp.Remove(once);
                ans[i] = once;
            }
            return ans;
        }
        public String[] Upset()
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
        public String[] GetAllValues()
        {
            String[] tmp = new String[set.Count()];
            set.CopyTo(tmp);
            return tmp;
        }
        public String GetCurrentFileName()
        {
            return file;
        }
        public void AddItem(String item)
        {
            set.Add(item);
        }
    }
}

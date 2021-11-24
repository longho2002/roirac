using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToanRoiRac_ck
{
    public class Graph
    {
        private const int SIZE = 50;
        public int[,] A = new int[SIZE, SIZE];
        public int n;
        protected int[] Danhdau = new int[SIZE];
        protected int[] Path = new int[SIZE];
        public int[] Min_Path = new int[SIZE];
        public int MIN = 99999;
        public int cmin = 99999;
        protected int cost = 0; // chi phis hien tai
        public int numOfCity = 0; // so thanh pho duoc chon
        public int[] city = new int[SIZE]; // danh dau cac thanh pho duoc chon
        public int city_; // luu lai so tp da di qua

        public int getSize()
        {
            return SIZE;
        }
        public Graph()
        {
            MIN = 99999;
            cmin = 99999;
            cost = 0;
            numOfCity = 0;
        }
        public Graph(int n)
        {
            this.n = n;
            MIN = 99999;
            cmin = 99999;
            cost = 0;
            numOfCity = 0;
        }
        private bool checkCity(int num)
        {
            bool flag = true;
            for (int i = 1; i < numOfCity; i++)
            {
                flag = false;
                for (int j = 0; j <= num; j++)
                {
                    if (city[i] == this.Path[j])
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    return false;
            }
            return true;
        }
        private void Process(int run, int num)
        {
            if (this.MIN < this.cost + (num - (run - 2)) * cmin)
                return;
            for (int i = 1; i <= this.n; i++)
            {
                if (run > num)
                {
                    if (this.cost < MIN && checkCity(num))
                    {
                        city_ = num;
                        MIN = this.cost;
                        for (int j = 2; j <= num; j++)
                        {
                            this.Min_Path[j - 1] = this.Path[j];
                        }
                    }
                    break;
                }
                else
                {
                    if (this.Danhdau[i] == 0)
                    {
                        this.Path[run] = i;
                        this.cost += this.A[this.Path[run - 1], this.Path[run]];
                        this.Danhdau[i] = 1;
                        Process(run + 1, num);
                        this.cost -= this.A[this.Path[run - 1], this.Path[run]];
                        this.Danhdau[i] = 0;
                    }
                }
            }
        }
        public void Dulich()
        {
            for (int i = numOfCity; i <= n; i++)
            {
                Danhdau[city[0]] = 1;
                Path[1] = city[0];
                Min_Path[0] = city[0];
                cost = 0;
                this.Process(2, i);
            }
        }

        public void DocFile(string[] s)
        {
            this.n = Int32.Parse(s[0]);
            for (int i = 1; i <= n; i++)
            {
                int c = 0;
                string[] tmp = s[i].Split(' ');
                for (int j = 1; j <= n; j++)
                {
                    A[i, j] = Int32.Parse(tmp[c++]);
                    A[i, j] = A[i, j] == 0 ? 99999 : A[i, j];
                    if (A[i, j] != 99999)
                        cmin = cmin > A[i, j] ? A[i, j] : cmin;
                }
            }
        }
    }
}

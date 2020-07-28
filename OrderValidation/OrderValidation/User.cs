using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderValidation
{
    class User
    {
        public string name;
        public string id;
        public string device;
        public string activity;
        public string notes;
        public double ppm;
        public int packed;
        public int pulled;
        public bool active;

        public User()
        {
            name = null;
            id = null;
            device = null;
            activity = null;
            notes = null;
        }

        ~User()
        {

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSmartHouse1._0
{
    class ChangeSetting : IChangeSettingAble
    {
        public int Increase(int argument)
        {
           return ++argument;
        }
        public int Decrease(int argument)
        {
            return --argument;
        }
        public int HandSet(int argument)
        {
            return argument;
        }
    }
}

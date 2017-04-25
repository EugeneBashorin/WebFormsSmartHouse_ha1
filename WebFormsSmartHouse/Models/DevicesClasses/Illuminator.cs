using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSmartHouse1._0
{
    class Illuminator : Device, IModeDefaultSettingsAble, IBrightAble<BrightnessLevel>
    {
        public BrightnessLevel Bright{
            get { return bright; }
            set { bright = value; }
        }
       
        private BrightnessLevel bright;
        public Illuminator(string name, bool state, BrightnessLevel bright) : base(name, state)
        {
            Name = name;
            State = state;
            this.bright = bright;
        }

        public void SetMaxMode()
        {
            Bright = BrightnessLevel.Bright;
        }

        public void SetMiddleMode()
        {
            Bright = BrightnessLevel.Medium;
        }

        public void SetMinMode()
        {
            Bright = BrightnessLevel.Low;
        }

        public void SetAutoMode()
        {
            Bright = BrightnessLevel.Default;
        }

        public override string ToString()
        {
            string bright;
            if (State)
            {
                if (this.Bright == BrightnessLevel.Bright)
                    bright = "Brirght";
                else if (this.Bright == BrightnessLevel.Medium)
                    bright = "Average";
                else if (this.Bright == BrightnessLevel.Low)
                    bright = "Soft";
                else
                    bright = "Auto";
            }
            else
            {
                bright = "--";
            }
            return base.ToString() + "<br />" + "Яркость:" + bright;
        }
    }
}
using System;
using System.Collections.Generic;

namespace SimpleSmartHouse1._0
{
    class Radio : Device, IChannelAble, IVolumeAble, ISetChannelAble
    {
        List<string> radioChannelList = new List<string>();
        public IChangeSettingAble ChangeParams { get; set; }
        public IParametrAble ChannelParam { get; set; }
        public IParametrAble VolumeParam { get; set; }
        public IListOrderAble ListFunction { get; set; }
        private string chanList = "Channel list:";
        private string readPath = AppDomain.CurrentDomain.BaseDirectory + @"ReadRadioChannel.txt";

        public int Channel {
            get { return ChannelParam.Current; }
            set { ChannelParam.Current = value; }
        }

        public int Volume {
            get { return VolumeParam.Current; }
            set { VolumeParam.Current = value; }
        }

        public Radio(string name, bool state, IParametrAble channelParam, IParametrAble volumeParam, IChangeSettingAble changeParams, IListOrderAble listFunction) : base (name, state)
        {
            State = state;
            ChannelParam = channelParam;
            VolumeParam = volumeParam;
            ChangeParams = changeParams;
            ListFunction = listFunction;
        }

        public override void SwtchOn()
        {
            State = true;
            ListFunction.ListLoad(radioChannelList, readPath);
        }

        public void LoadChannel()
        {
            ListFunction.ListLoad(radioChannelList, readPath);
        }

        public void IncreaseVolume()
        {
            Volume = ChangeParams.Increase(Volume);
        }

        public void DecreaseVolume()
        {
            Volume = ChangeParams.Decrease(Volume);
        }

        public void HandSetVolume(int inputData)
        {
            Volume = ChangeParams.HandSet(inputData);
        }
        //Channel
        public void IncreaseChannel()
        {
            Channel = ChangeParams.Increase(Channel);
        }

        public void DecreaseChannel()
        {
            Channel = ChangeParams.Decrease(Channel);
        }

        public void HandSetChannel(int inputData)
        {
            Channel = ChangeParams.HandSet(inputData);
        }

        public string ShowChannelList()
        {
            return ListFunction.ShowList(radioChannelList, chanList);
        }

        public override string ToString()
        {
            string channelName;
            string channelNumber;
            string volume;
            if (State)
            {
                channelName = radioChannelList[Channel];
                channelNumber = Channel.ToString();
                volume = Volume.ToString();
            }
            else
            {
                channelName = "--";
                channelNumber = "--";
                volume = "--";
            }
            return base.ToString() + "<br />" + "Channel: " + channelNumber + " : " + channelName + "<br />" + "Volume: " + volume;
        }
    }
}
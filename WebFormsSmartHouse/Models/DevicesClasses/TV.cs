using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleSmartHouse1._0
{
    class TV : Device, IModeDefaultSettingsAble, IChannelAble, ISetChannelAble, IVolumeAble, IBrightAble<BrightnessLevel>
    {
        List<string> tvChannelList = new List<string>();
        private BrightnessLevel bright { get; set; }
        public IChangeSettingAble ChangeParams { get; set; }
        public IParametrAble ChannelParam { get; set; }
        public IParametrAble VolumeParam { get; set; }
        public IListOrderAble ListFunction { get; set; }
        private string chanList = "Channel list:";
        private string readPath = AppDomain.CurrentDomain.BaseDirectory + @"ReadTVChannel.txt";

        public BrightnessLevel Bright{
            get { return bright; }
            set { bright = value; }
        }

        public int Channel {
            get {return ChannelParam.Current;}
            set { ChannelParam.Current = value;}
        }

        public int Volume {
            get {return VolumeParam.Current; }
            set { VolumeParam.Current = value; }
        }

        public TV(string name, bool state, IParametrAble channelParam, IParametrAble volumeParam, IChangeSettingAble changeParams, BrightnessLevel bright, IListOrderAble listFunction) : base (name, state)
        {
            Name = name;
            State = state;
            ChannelParam = channelParam;
            VolumeParam = volumeParam;
            ChangeParams = changeParams;
            this.bright = bright;
            ListFunction = listFunction;
        }
        public void LoadChannel()
        {
            ListFunction.ListLoad(tvChannelList, readPath);
        }

        //Volume
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
            return ListFunction.ShowList(tvChannelList, chanList);
        }

        //BrightnessLevel
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
            string channelName;
            string channelNumber;
            string volume;
            string bright;
            if (State)
            {
                channelName = tvChannelList[Channel];
                channelNumber = Channel.ToString();
                volume = Volume.ToString();
                bright = Bright.ToString();
            }
            else
            {
                channelName = "--";
                channelNumber = "--";
                volume = "--";
                bright = "--";
            }
            return base.ToString() + "<br />" + "Channel: " + channelNumber + " : " + channelName + "<br />" + "Volume: " + volume + "<br />" + "Bright: " + bright;
        }
    }
}
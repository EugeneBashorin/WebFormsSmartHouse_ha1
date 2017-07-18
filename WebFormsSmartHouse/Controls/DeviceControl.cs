using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SimpleSmartHouse1._0;
using System;
using System.Web;

namespace WebFormsSmartHouse.Controls
{
    public class DeviceControl : Panel
    {
        private int id;
        private int temp;
        private IDictionary<int, Device> deviceDictionary;
        //Buttons
        private Button switchOnButton;      // On
        private Button switchOffButton;     // Off
        private Button deleteButton;        // Delete
        private Button setTemperature;      // Temperature
        private Button increaseTemperature; // Temperature
        private Button decreaseTemperature; // Temperature
        private Button setVolume;           // Volume
        private Button incVolume;           // Volume
        private Button decVolume;           // Volume
        private Button setChannel;          // Channel
        private Button nextChannel;         // Channel
        private Button prevChannel;         // Channel
        private Button setBrightMode;       // Bright       
        private Button setCommonMode;       // Mode
        //Mode    
        private DropDownList commonMode;
        private DropDownList brightnessLevel;

        private TextBox textAreaList;
        private PlaceHolder placeHolderList;
        private TextBox channelTextBox; // Поле ввода для отображения/установки значения
        private TextBox temperatureTextBox; // Поле ввода для отображения/установки значения Temperature
        private TextBox volumeTextBox; // Поле ввода для отображения/установки значения Temperature
        //ErrBlock
        private PlaceHolder placeHolderVolError;
        private PlaceHolder placeHolderChanError;
        private PlaceHolder placeHolderTemperError;

        private Image icon;

        public DeviceControl(int id, IDictionary<int, Device> deviceDictionary)
        {
            this.id = id;
            this.deviceDictionary = deviceDictionary;
            Initializer();
        }

        protected void Initializer()
        {
            CssClass = "device-div";
            Controls.Add(Span("Device: " + deviceDictionary[id].Name + "<br />" + deviceDictionary[id] + "<br/>"));
            icon = new Image();

            if (deviceDictionary[id] is Device)
            {
                switchOnButton = ButtonData("on", "On");
                switchOnButton.Click += switchOnButtonClick;
                Controls.Add(switchOnButton);

                switchOffButton = ButtonData("off", "Off");
                switchOffButton.Click += switchOffButtonClick;
                Controls.Add(switchOffButton);

                deleteButton = ButtonData("del", "Delete device");
                deleteButton.Click += DeleteButtonClick;
                Controls.Add(deleteButton);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id] is TV)
            {
                if (deviceDictionary[id].State)
                {
                    icon.ImageUrl = "App_Themes/TVon.png";
                }
                else
                {
                    icon.ImageUrl = "App_Themes/TVoff.png";
                }
                Controls.Add(icon);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id] is Radio)
            {
                if (deviceDictionary[id].State)
                {
                    icon.ImageUrl = "App_Themes/radioOn.png";
                }
                else
                {
                    icon.ImageUrl = "App_Themes/radioOff.png";
                }
                Controls.Add(icon);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id] is Illuminator)
            {
                if (deviceDictionary[id].State)
                {
                    icon.ImageUrl = "App_Themes/onLamp.png";
                }
                else
                {
                    icon.ImageUrl = "App_Themes/offLamp.png";
                }
                Controls.Add(icon);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id].Name == "Heater")
            {
                if (deviceDictionary[id].State)
                {
                    icon.ImageUrl = "App_Themes/RadiatorOn.png";
                }
                else
                {
                    icon.ImageUrl = "App_Themes/RadiatorOff.png";
                }
                Controls.Add(icon);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id].Name == "AirCondition")
            {
                if (deviceDictionary[id].State)
                {
                    icon.ImageUrl = "App_Themes/onAC.png";
                }
                else
                {
                    icon.ImageUrl = "App_Themes/offAC.png";
                }
                Controls.Add(icon);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id] is ITemperatureAble)
            {
                setTemperature = ButtonData("temper", "Set temp");
                setTemperature.Click += SetTemperature_Click;
                Controls.Add(setTemperature);

                temperatureTextBox = TextBox("");
                temperatureTextBox.ID = "Temperature" + id.ToString();
                Controls.Add(temperatureTextBox);
                Controls.Add(Span("<br />"));

                increaseTemperature = ButtonData("incTem", "Temp +");
                increaseTemperature.Click += IncreaseTemperature_Click;
                Controls.Add(increaseTemperature);

                decreaseTemperature = ButtonData("decTem", "Temp -");
                decreaseTemperature.Click += DecreaseTemperature_Click;
                Controls.Add(decreaseTemperature);
                Controls.Add(Span("<br />"));

                placeHolderTemperError = PlaceHolderData("placeHolderTemperError");
                Controls.Add(placeHolderTemperError);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id] is IBrightAble<BrightnessLevel>)
            {
                setBrightMode = new Button();
                setBrightMode.ID = "br" + id.ToString();
                setBrightMode.Text = "Set bright";
                setBrightMode.Click += SetBrightMode_Click;
                Controls.Add(setBrightMode);

                brightnessLevel = DropDownListData("brightMode");
                brightnessLevel.Items.Add(BrightnessLevel.Bright.ToString());
                brightnessLevel.Items.Add(BrightnessLevel.Medium.ToString());
                brightnessLevel.Items.Add(BrightnessLevel.Low.ToString());
                brightnessLevel.Items.Add(BrightnessLevel.Default.ToString());

                if (HttpContext.Current.Session["BrightMode"] != null)
                {
                    brightnessLevel.SelectedIndex = (int)HttpContext.Current.Session["BrightMode"];
                }
                Controls.Add(brightnessLevel);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id] is IVolumeAble)
            {
                setVolume = ButtonData("vol", "Set volume");
                setVolume.Click += SetVolume_Click;
                Controls.Add(setVolume);

                volumeTextBox = TextBox("");
                volumeTextBox.ID = "Volume" + id.ToString();
                Controls.Add(volumeTextBox);
                Controls.Add(Span("<br />"));

                incVolume = ButtonData("incVol", "Volume +");
                incVolume.Click += IncreaseVolume_Click;
                Controls.Add(incVolume);

                decVolume = ButtonData("decVol", "Volume -");
                decVolume.Click += DecreaseVolume_Click;
                Controls.Add(decVolume);
                Controls.Add(Span("<br />"));

                placeHolderVolError = PlaceHolderData("placeHolderVolError");
                Controls.Add(placeHolderVolError);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id] is IChannelAble)
            {
                setChannel = ButtonData("chan", "Set channel");
                setChannel.Click += SetChannel_Click;
                Controls.Add(setChannel);

                channelTextBox = TextBox("");
                channelTextBox.ID = "Channel" + id.ToString();
                Controls.Add(channelTextBox);
                Controls.Add(Span("<br />"));

                nextChannel = ButtonData("nxtCh", "Channel +");
                nextChannel.Click += NextChannel_Click;
                Controls.Add(nextChannel);

                prevChannel = ButtonData("prvCh", "Channel -");
                prevChannel.Click += PrevChannel_Click;
                Controls.Add(prevChannel);
                Controls.Add(Span("<br />"));

                placeHolderChanError = PlaceHolderData("placeHolderChanError");
                Controls.Add(placeHolderChanError);
                Controls.Add(Span("<br />"));
            }

            if (deviceDictionary[id] is ISetChannelAble)
            {
                if (deviceDictionary[id].State)
                {
                    placeHolderList = PlaceHolderData("pHL");
                    Button showChannelList = ButtonData("chanList", "Chan list");
                    showChannelList.Click += ShowChannelList_Click;
                    Controls.Add(showChannelList);
                    Controls.Add(Span("<br />"));
                    Controls.Add(placeHolderList);
                }
            }

            if (deviceDictionary[id] is IModeAble<Mode>)
            {
                setCommonMode = ButtonData("mode", "Set mode");
                setCommonMode.Click += SetCommonMode_Click;
                Controls.Add(setCommonMode);

                commonMode = DropDownListData("CommonMode"); 
                commonMode.Items.Add(Mode.Turbo.ToString());
                commonMode.Items.Add(Mode.Eco.ToString());
                commonMode.Items.Add(Mode.Low.ToString());
                commonMode.Items.Add(Mode.Auto.ToString());

                if (HttpContext.Current.Session["CommonMode"] != null)
                {
                    commonMode.SelectedIndex = (int)HttpContext.Current.Session["CommonMode"];
                }
                Controls.Add(commonMode);
                Controls.Add(Span("<br />"));
            }

        }

        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
        }

        protected TextBox TextBox(string value)
        {
            TextBox textBox = new TextBox();
            textBox.Text = value;
            textBox.Columns = 3;
            return textBox;
        }

        protected Button ButtonData(string butID, string butText)
        {
            Button button = new Button();
            button.ID = butID + id.ToString();
            button.Text = butText;
            return button;
        }

        protected PlaceHolder PlaceHolderData(string placeHolderID)
        {
            PlaceHolder placeHolder = new PlaceHolder();
            placeHolder.ID = placeHolderID + id.ToString();
            return placeHolder;
        }

        protected DropDownList DropDownListData(string dropDownListID)
        {
            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = dropDownListID + id.ToString();
            return dropDownList;
        }

        private void SetBrightMode_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["BrightMode"] = brightnessLevel.SelectedIndex;
            IModeDefaultSettingsAble mode = (IModeDefaultSettingsAble)deviceDictionary[id];
            switch (brightnessLevel.SelectedIndex)
            {
                case 0:
                    mode.SetMaxMode();
                    break;
                case 1:
                    mode.SetMiddleMode();
                    break;
                case 2:
                    mode.SetMinMode();
                    break;
                default:
                    mode.SetAutoMode();
                    break;
            }
            Controls.Clear();
            Initializer();
        }

        private void SetCommonMode_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["CommonMode"] = commonMode.SelectedIndex;
            IModeDefaultSettingsAble mode = (IModeDefaultSettingsAble)deviceDictionary[id];
            switch (commonMode.SelectedIndex)
            {
                case 0:
                    mode.SetMaxMode();
                    break;
                case 1:
                    mode.SetMiddleMode();
                    break;
                case 2:
                    mode.SetMinMode();
                    break;
                default:
                    mode.SetAutoMode();
                    break;
            }
            Controls.Clear();
            Initializer();
        }

        protected void switchOnButtonClick(object sender, EventArgs e)
        {
            if (deviceDictionary[id] is Device)
            {
                deviceDictionary[id].SwtchOn();
            }
            if (deviceDictionary[id] is ISetChannelAble)
            {
                ((ISetChannelAble)deviceDictionary[id]).LoadChannel();
            }
            Controls.Clear();
            Initializer();
        }

        protected void switchOffButtonClick(object sender, EventArgs e)
        {
            if (deviceDictionary[id] is Device)
            {
                deviceDictionary[id].SwtchOff();
            }
            Controls.Clear();
            Initializer();
        }

        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            deviceDictionary.Remove(id);
            Parent.Controls.Remove(this);
        }
        //VOLUME//
        protected void SetVolume_Click(object sender, EventArgs e)
        {
            Label volError = new Label();
            volError.Text = "";
            volError.ID = "volError" + id.ToString();
            volError.CssClass = "blockError";

            if (deviceDictionary[id].State)
            {
                if (deviceDictionary[id] is IVolumeAble)
                {
                    if (Int32.TryParse(volumeTextBox.Text, out temp))
                    {
                        ((IVolumeAble)deviceDictionary[id]).HandSetVolume(temp);
                    }
                    else
                    {
                        volError.Text = "Not a number";
                    }
                }
            }
            Controls.Clear();
            Initializer();
            placeHolderVolError.Controls.Add(volError);
        }

        protected void IncreaseVolume_Click(object sender, EventArgs e)
        {
            if (deviceDictionary[id].State)
            {
                if (deviceDictionary[id] is IVolumeAble)
                {
                    if (deviceDictionary[id] is IVolumeAble)
                        ((IVolumeAble)deviceDictionary[id]).IncreaseVolume();
                }
            }
            Controls.Clear();
            Initializer();
        }

        protected void DecreaseVolume_Click(object sender, EventArgs e)
        {
            if (deviceDictionary[id].State)
            {
                if (deviceDictionary[id] is IVolumeAble)
                {
                    if (deviceDictionary[id] is IVolumeAble)
                        ((IVolumeAble)deviceDictionary[id]).DecreaseVolume();
                }
            }
            Controls.Clear();
            Initializer();
        }
        //CHANNEL//
        protected void SetChannel_Click(object sender, EventArgs e)
        {
            Label chanError = new Label();
            chanError.Text = "";
            chanError.ID = "chanError" + id.ToString();
            chanError.CssClass = "blockError";
            if (deviceDictionary[id].State)
            {
                if (deviceDictionary[id] is IChannelAble)
                {
                    if (Int32.TryParse(channelTextBox.Text, out temp))
                    {
                        ((IChannelAble)deviceDictionary[id]).HandSetChannel(temp);
                    }
                    else
                    {
                        chanError.Text = "Not a number";
                    }
                }
            }
            Controls.Clear();
            Initializer();
            placeHolderChanError.Controls.Add(chanError);
        }

        protected void NextChannel_Click(object sender, EventArgs e)
        {
            if (deviceDictionary[id].State)
            {
                if (deviceDictionary[id] is IChannelAble)
                {
                    ((IChannelAble)deviceDictionary[id]).IncreaseChannel();
                }
            }
            Controls.Clear();
            Initializer();
        }

        protected void PrevChannel_Click(object sender, EventArgs e)
        {
            if (deviceDictionary[id].State)
            {
                if (deviceDictionary[id] is IChannelAble)
                {
                    ((IChannelAble)deviceDictionary[id]).DecreaseChannel();
                }
            }
            Controls.Clear();
            Initializer();
        }

        protected void ShowChannelList_Click(object sender, EventArgs e)
        {
            textAreaList = new TextBox();
            textAreaList.ID = "textAreaList" + id.ToString();
            textAreaList.Columns = 4;
            textAreaList.Rows = 10;
            textAreaList.TextMode = TextBoxMode.MultiLine;
            textAreaList.CssClass = "textAreaList";
            textAreaList.Text += ((ISetChannelAble)deviceDictionary[id]).ShowChannelList();
            Controls.Clear();
            Initializer();
            placeHolderList.Controls.Add(textAreaList);
        }

        //TEMPERATURE//
        protected void SetTemperature_Click(object sender, EventArgs e)
        {
            Label temperError = new Label();
            temperError.Text = "";
            temperError.ID = "temperError" + id.ToString();
            temperError.CssClass = "blockError";
            if (deviceDictionary[id].State)
            {
                if (deviceDictionary[id] is ITemperatureAble)
                {
                    if (Int32.TryParse(temperatureTextBox.Text, out temp))
                    {
                        ((ITemperatureAble)deviceDictionary[id]).HandSetTemperature(temp);
                    }
                    else
                    {
                        temperError.Text = "Not a number";
                    }
                }
            }
            Controls.Clear();
            Initializer();
            placeHolderTemperError.Controls.Add(temperError);
        }

        protected void IncreaseTemperature_Click(object sender, EventArgs e)
        {
            if (deviceDictionary[id].State)
            {
                if (deviceDictionary[id] is ITemperatureAble)
                {
                    ((ITemperatureAble)deviceDictionary[id]).IncreaseTemperature();
                }
            }
            Controls.Clear();
            Initializer();
        }

        protected void DecreaseTemperature_Click(object sender, EventArgs e)
        {
            if (deviceDictionary[id].State)
            {
                if (deviceDictionary[id] is ITemperatureAble)
                {
                    ((ITemperatureAble)deviceDictionary[id]).DecreaseTemperature();
                }
            }
            Controls.Clear();
            Initializer();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsSmartHouse.Controls;
using System.Web.UI.HtmlControls;
using SimpleSmartHouse1._0;

namespace WebFormsSmartHouse
{
    public partial class Default : System.Web.UI.Page
    {
        private IDictionary<int, Device> deviceDictionary;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                deviceDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            }
            else
            {
                deviceDictionary = new SortedDictionary<int, Device>();
                deviceDictionary.Add(1, new TV("TV", false, new Parametr(0, 50, 3), new Parametr(1, 45, 12), new ChangeSetting(), BrightnessLevel.Default, new ListFunction()));
                deviceDictionary.Add(2, new Radio("Radio", false, new Parametr(0, 14, 3), new Parametr(1, 45, 10), new ChangeSetting(), new ListFunction()));                
                deviceDictionary.Add(3, new Heater("Heater", false, Mode.Eco, new Parametr(15, 34, 18), new ChangeSetting()));
                deviceDictionary.Add(4, new AirCondition("AirCondition", false, Mode.Low, new Parametr(8, 26, 18), new ChangeSetting()));
                deviceDictionary.Add(5, new Illuminator("Illuminator", false, BrightnessLevel.Medium));

                Session["Devices"] = deviceDictionary;
                Session["NextId"] = 6;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                addDeviceButton.Click += AddDeviceButtonClick;
            }
            InitialiseDevicePanel();
        }

        public void InitialiseDevicePanel()
        {
            foreach (int key in deviceDictionary.Keys)
            {
              devicePanel.Controls.Add(new DeviceControl(key, deviceDictionary));
            }
        }

        protected void AddDeviceButtonClick(object sender, EventArgs e)
        {
            Device newDevice;
            switch (dropDownDeviceList.SelectedIndex)
            {
                default:
                    newDevice = new TV("TV", false, new Parametr(1, 100, 3), new Parametr(1, 45, 15), new ChangeSetting(), BrightnessLevel.Default, new ListFunction());
                    break;
                case 1:
                    newDevice = new Radio("Radio", false, new Parametr(1, 100, 3), new Parametr(1, 45, 15), new ChangeSetting(), new ListFunction());
                    break;
                case 2:
                    newDevice = new Heater("Heater", false, Mode.Eco, new Parametr(8, 15, 12), new ChangeSetting());
                    break;
                case 3:
                    newDevice = new AirCondition("AirCondition", false, Mode.Low, new Parametr(8, 15, 12), new ChangeSetting());
                    break;
                case 4:
                    newDevice = new Illuminator("Illuminator", false, BrightnessLevel.Medium);
                    break;
            }
            int id = (int)Session["NextId"];
            deviceDictionary.Add(id, newDevice);
            devicePanel.Controls.Add(new DeviceControl(id, deviceDictionary));
            id++;
            Session["NextId"] = id;
        }
    }
 }
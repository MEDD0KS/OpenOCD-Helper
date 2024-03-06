using System.Configuration;
using System.Data;
using System.Windows;

namespace OpenOCD_Helper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            JsonSettings jsonSettings = new JsonSettings()
            {
                NameInterface = "cmsis-dap.cfg",
                NameTarget = "stm32f4x.cfg",
                PathFlashReadFile = "",
                PathFlashWriteFile = "",
                ValueFlashStartAdr = 0x08000000,
                ValueFlashOffset = 0x0,
                TypeTransportInterface = "swd",
                PathOpenOCD = "1",
                PathInterface = "2",
                PathTarget = "3"
            };

            JsonFileParser.Serialize(SettingsPaths.pathJsonFileSettings, jsonSettings);

            var readSettings = JsonFileParser.Deserialize<JsonSettings>(SettingsPaths.pathJsonFileSettings);
        }
    }

}

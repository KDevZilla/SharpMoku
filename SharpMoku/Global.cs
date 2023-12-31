using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{
    public class Global
    {
        private static SharpMokuSettings _CurrentSettings = null;
        public static SharpMokuSettings CurrentSettings
        {
            get
            {

                if (_CurrentSettings == null)
                {
                    if (!System.IO.File.Exists(Utility.FileUtility.SettingPath))
                    {
                        Utility.SerializeUtility.CreateNewSettings(Utility.FileUtility.SettingPath);
                    }

                    _CurrentSettings = Utility.SerializeUtility.DeserializeSettings(Utility.FileUtility.SettingPath);

                }
                return _CurrentSettings;
            }
        }
        public static void SaveSettings()
        {

            Utility.SerializeUtility.SerializeSettings(_CurrentSettings, Utility.FileUtility.SettingPath);

            _CurrentSettings = null;
        }

        public static Color BackColor => UI.ThemeSpace.ThemeFactory.BackColor(CurrentSettings.ThemeEnum);
        public static Color ForeColor => UI.ThemeSpace.ThemeFactory.ForeColor(CurrentSettings.ThemeEnum);
    }
}

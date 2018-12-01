using System.Data;

namespace Business.Settings
{
    public class UserSettings
    {
        public UserSettings()
        {
        }

        public void Save(Entity.Settings.UserSettings userSettings)
        {
            DataAccess.Settings.UserSettings.Save(userSettings);
        }

        public DataSet GetByUserId(int userId)
        {
            return DataAccess.Settings.UserSettings.GetByUserId(userId);
        }
    }
}

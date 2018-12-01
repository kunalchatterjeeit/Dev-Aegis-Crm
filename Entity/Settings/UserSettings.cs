namespace Entity.Settings
{
    public class UserSettings
    {
        public UserSettings() { }
        public int UserId { get; set; }
        public string SettingName { get; set; }
        public int SettingValue { get; set; }
        public bool IsActive { get; set; }
        public string SettingValues { get; set; }
    }
}

namespace autumn.ml;

public class Initalizers
{
    public static class ConfigSystem
    {
        public static void SetConfig()
        {
            Entry.config = Config.LoadFromFile("config.json");
        }
        public static void Save()
        {
            Config.SaveToFile(Entry.config, "config.json");
        }
    }
    public static class Cheat
    {
        public static void Execute()
        {
            InfoAboutCheat.Tooltips.Initalize();
        }
    }
}
 
namespace autumn.ml;

public class Initalizers
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

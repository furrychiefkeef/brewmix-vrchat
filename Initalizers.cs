namespace autumn.ml;

public class Initalizers
{
    public static void SetConfig()
    {
        Entry.config = Config.LoadFromFile("config.json");
    }
}

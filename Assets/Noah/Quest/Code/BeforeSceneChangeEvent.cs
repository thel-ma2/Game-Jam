public class BeforeSceneChangeEvent
{
    public string NextSceneName { get; private set; }

    public BeforeSceneChangeEvent(string nextSceneName)
    {
        NextSceneName = nextSceneName;
    }
}

public struct SceneChangedEvent
{
    public string sceneName;
    // ist für das merken eines scenen wechsels verantwortlich
    public SceneChangedEvent(string name)
    {
        sceneName = name;
    }
}

public struct SceneChangedEvent
{
    public string sceneName;
    // ist f�r das merken eines scenen wechsels verantwortlich
    public SceneChangedEvent(string name)
    {
        sceneName = name;
    }
}

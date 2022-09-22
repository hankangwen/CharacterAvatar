using UnityEditor;

public class EditorDeveloperMode
{
    private const string KeyDeveloperMode = "DeveloperMode";

    private static bool IsEnable
    {
        get => EditorPrefs.GetBool(KeyDeveloperMode, false);
        set => EditorPrefs.SetBool(KeyDeveloperMode, value);
    }

    [MenuItem("Tools/编辑器开发者模式/Enable")]
    private static void EnableDeveloperMode()
    {
        IsEnable = true;
    }

    [MenuItem("Tools/编辑器开发者模式/Enable", true)]
    private static bool EnableDeveloperModeCheck()
    {
        return !IsEnable;
    }

    [MenuItem("Tools/编辑器开发者模式/Disable")]
    private static void DisableDeveloperMode()
    {
        IsEnable = false;
    }

    [MenuItem("Tools/编辑器开发者模式/Disable", true)]
    private static bool DisableDeveloperModeCheck()
    {
        return IsEnable;
    }
}
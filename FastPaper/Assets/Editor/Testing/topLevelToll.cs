using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class topLevelToll{
	[MenuItem("Tools/ay",false,2)]
	private static void NewMenuOption()
	{
        Debug.Log("ay");
    }
    [MenuItem("Tools/lmao",false, 1)]
    private static void NewMenuOption2()
    {
        Debug.Log("lmao");
    }


    [MenuItem("Window/New Option")]
	private static void NewWindow()
	{
	}

[MenuItem("Assets/Load Additive Scene")]
    private static void LoadAdditiveScene()
    {
        var selected = Selection.activeObject;
//        SceneManager.EditorOpenScene(AssetDatabase.GetAssetPath(selected), OpenSceneMode.Additive);
    }

[MenuItem("Assets/Create/Add Configuration")]
    private static void AddConfig()
    {
        // Create and add a new ScriptableObject for storing configuration
    }

[MenuItem("CONTEXT/Rigidbody/New Option")]
    private static void NewOpenForRigidBody()
    {
    }
}

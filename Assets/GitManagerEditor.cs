using UnityEngine;
using UnityEditor;
public class GitManagerEditor : EditorWindow
{
	public GUISkin skin;
	void OnEnable()
	{
		GitManager.Log();
	}
	bool showLocal = true;
	bool showRemote = true;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/My Window")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		GitManagerEditor window = (GitManagerEditor)EditorWindow.GetWindow(typeof(GitManagerEditor));
		window.Show();
	}

	void OnGUI()
	{
		GUI.skin = skin;

		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		if (GUILayout.Button("Commit", EditorStyles.toolbarButton))
		{

		}
		if (GUILayout.Button("Push", EditorStyles.toolbarButton))
		{

		}
		if (GUILayout.Button("Fetch", EditorStyles.toolbarButton))
		{
			GitManager.Log();
		}
		if (GUILayout.Button("Pull", EditorStyles.toolbarButton))
		{

		}

		GUILayout.EndHorizontal();

		showRemote = EditorGUILayout.Foldout(showRemote, "Remote", true);
		if (showRemote)
		{
			foreach (GitFile file in GitManager.fetch)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label(file.path);
				GUILayout.Label(file.additions.ToString(), "Additions");
				GUILayout.Label(file.deletions.ToString(), "Deletions");
				GUILayout.EndHorizontal();
			}
		}
		showLocal = EditorGUILayout.Foldout(showLocal, "Local", true);
		if (showLocal)
		{
			foreach (GitFile file in GitManager.head)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label(file.path);
				GUILayout.Label(file.additions.ToString(), "Additions");
				GUILayout.Label(file.deletions.ToString(), "Deletions");
				GUILayout.EndHorizontal();
			}
		}
	}
}

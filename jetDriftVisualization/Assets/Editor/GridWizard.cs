using UnityEditor;
using UnityEngine;

public class GridWizard : ScriptableWizard {
	
	[MenuItem("Assets/Create/Grid")]
	private static void CreateWizard () {
		ScriptableWizard.DisplayWizard<GridWizard>("Create Grid");
	}
	
	[Range(1, 128)]
	public int width = 32;
    public bool twoSided;
	
	private void OnWizardCreate () {
		string path = EditorUtility.SaveFilePanelInProject("Save Grid", "Grid", "asset", "Specify where to save the mesh.");
		if (path.Length > 0) {
			Mesh mesh = GridCreator.CreateGrid(width, twoSided);
			MeshUtility.Optimize(mesh);
			AssetDatabase.CreateAsset(mesh, path);
			Selection.activeObject = mesh;
		}
	}
}
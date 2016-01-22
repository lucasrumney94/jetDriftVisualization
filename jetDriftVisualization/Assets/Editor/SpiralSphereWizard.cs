using UnityEditor;
using UnityEngine;

public class SpiralSphereWizard : ScriptableWizard {
	
	[MenuItem("Assets/Create/Spiral Sphere")]
	private static void CreateWizard () {
		ScriptableWizard.DisplayWizard<SpiralSphereWizard>("Create Spiral Sphere");
	}
	
	[Range(1, 128)]
	public int width = 20;
	public float radius = 1f;
	
	private void OnWizardCreate () {
		string path = EditorUtility.SaveFilePanelInProject("Save Spiral Sphere", "Spiral Sphere", "asset", "Specify where to save the mesh.");
		if (path.Length > 0) {
			Mesh mesh = SphereCreator.CreateSpiralSphere(width, radius);
			MeshUtility.Optimize(mesh);
			AssetDatabase.CreateAsset(mesh, path);
			Selection.activeObject = mesh;
		}
	}
}
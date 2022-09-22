using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class GenerateSkinDataEditor : EditorWindow
{
    private GameObject _prefab;
    private static bool _reShow = false;
    
    [MenuItem("Assets/选中Prefab生成时装数据")]
    public static void ShowWindow()
    {
        _reShow = true;
        GetWindow<GenerateSkinDataEditor>("选中Prefab生成时装数据").Show();
    }

    private void OnFocus()
    {
        if (!_reShow) return;
        _reShow = false;
        _prefab = Selection.GetFiltered(typeof(Object), SelectionMode.Assets)[0] as GameObject;
    }

    private void OnGUI()
    {
        _prefab = EditorGUILayout.ObjectField("预制体", _prefab, typeof(Object), true) as GameObject;

        if (GUILayout.Button("生成时装数据"))
        {
            GenerateSkinData();
        }
    }

    private void GenerateSkinData()
    {
        string path = Application.dataPath + "/Resources/";
        SkinnedMeshRenderer[] parts = _prefab.GetComponentsInChildren<SkinnedMeshRenderer>();
        
        foreach (var part in parts)
        {
            SaveMesh(part.sharedMesh);
            SaveSmr2Txt(part, _prefab.name, path);
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void SaveMesh(Mesh mesh)
    {
        string mathSavePath = $"Assets/Resources/mesh/{mesh.name}.asset";
        Mesh meshToSave = Instantiate(mesh) as Mesh;
        MeshUtility.Optimize(meshToSave);
        AssetDatabase.CreateAsset(meshToSave, mathSavePath);
        
    }
    
    private void SaveSmr2Txt(SkinnedMeshRenderer smr, string objName, string path)
    {
        if (smr.rootBone == null)
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        int materialCount = smr.sharedMaterials.Length;
        // 第1行存储material数量，设为n
        sb.Append(materialCount + "\n");
        
        // 第2行开始存储material的名字
        for (int i = 0; i < materialCount; i++)
        {
            sb.Append(smr.sharedMaterials[i].name + "\n");
        }
        
        // 第n+2行存储mesh的名字
        sb.Append(smr.sharedMesh.name + "\n");
        
        // 第n+3行存储rootBon
        sb.Append(GetRoute(smr.rootBone, objName));
        
        // 第n+4行开始都是bones
        foreach (Transform bone in smr.bones)
        {
            sb.Append("\n" + GetRoute(bone, objName));
        }

        string txtPath = $"{path}txt/{smr.name}.txt";
        OnStreamWriter(txtPath, sb.ToString());
    }
    
    private string GetRoute(Transform tran, string objName, string splitter = "/")
    {
        var result = $"{tran.name}";
        var parent = tran.parent;
        while (parent != null && parent.name != objName)
        {
            result = $"{parent.name}{splitter}{result}";
            parent = parent.parent;
        }

        return result;
    }
    
    private void OnStreamWriter(string path, string content)
    {
        if (File.Exists(path))
            File.Delete(path);
        using (StreamWriter w = new StreamWriter(path, false, Encoding.UTF8))
            w.Write(content);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSys : MonoBehaviour
{
    private Transform _girlSourceTrans;
    private GameObject _girlTarget;     // 骨架物体，换装的人
    private Transform[] _girlHips;      //girl的骨骼信息
    
    /// <summary>
    /// 换装骨骼girlTarget上的skm信息
    /// </summary>
    private Dictionary<string, SkinnedMeshRenderer> _girlSmr = new Dictionary<string, SkinnedMeshRenderer>();
    
    /// <summary>
    /// girl的所有资源信息
    /// </summary>
    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> _girlData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
    
    //初始化信息，部位的名字，部位对应的skm
    private string[,] _girlStr = { {"eyes","1"},{"hair","1"},{"top","1"},{"pants","1"},{"shoes","1"},{"face","1"}};

    #region Test
    public string[] girlSkins = {"eyes-2", "hair-2", "top-2", "pants-2", "shoes-2", "face-2"};

    void RefreshAvatar()
    {
        foreach (var str in girlSkins)
        {
            // var array = str.Split('-');
            // ChangeMesh(array[0], array[1]);
            ChangeMesh2(str);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RefreshAvatar();
        }
    }

    void ChangeMesh2(string partName)
    {
        var textAsset = Resources.Load<TextAsset>($"txt/{partName}");
        string[] lines = textAsset.text.Split('\n');
        
        int materialCount = Int32.Parse(lines[0]);
        Material[] materials = new Material[materialCount];
        for (int i = 0; i < materialCount; i++)
        {
            string materialName = lines[1 + i];
            materials[i] = Resources.Load<Material>($"material/{materialName}");
        }

        string meshName = lines[materialCount + 1];
        Mesh mesh = Resources.Load<Mesh>($"mesh/{meshName}");

        var targetTran = _girlTarget.transform;
        var rootBone = targetTran.Find(lines[materialCount + 2]);
        
        var boneList = new List<Transform>();
        for (int i = materialCount + 3; i < lines.Length; i++)
        {
            boneList.Add(targetTran.Find(lines[i]));
        }
        
        //换装实现
        string part = partName.Split('-')[0];
        SkinnedMeshRenderer curSkm;
        if (!_girlSmr.TryGetValue(part, out curSkm))
        {
            GameObject partGo = new GameObject(part);
            partGo.transform.parent = _girlTarget.transform;
            _girlSmr.Add(part, partGo.AddComponent<SkinnedMeshRenderer>());
            curSkm = _girlSmr[part];
        }

        curSkm.rootBone = rootBone;
        curSkm.bones = boneList.ToArray();
        curSkm.materials = materials;
        curSkm.sharedMesh = mesh;
    }
    #endregion
     
    private void Start()
    {
        // InstantiateSource();
        InstantiateTarget();
        
        // SaveData();
        // InitAvatar();
    }

    private void InstantiateSource()
    {
        GameObject go = Instantiate(Resources.Load("FemaleModel")) as GameObject;
        if (!go) return;
        _girlSourceTrans = go.transform;
        go.SetActive(false);
    }

    private void InstantiateTarget()
    {
        _girlTarget = Instantiate(Resources.Load("FemaleTarget")) as GameObject;
        if (!_girlTarget) return;
        _girlHips = _girlTarget.GetComponentsInChildren<Transform>();
    }

    void SaveData()
    {
        if (_girlSourceTrans == null) return;

        //找到sourceTran中所有的SkinnedMeshRenderer，进行存储
        SkinnedMeshRenderer[] parts = _girlSourceTrans.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var part in parts)
        {
            string[] names = part.name.Split('-');
            var partName = names[0];
            if (!_girlData.ContainsKey(partName))
            {
                // 生成对应的部位，且只生成一个
                GameObject partGo = new GameObject(partName);
                partGo.transform.parent = _girlTarget.transform;
                // 把骨骼target身上的skm信息存储
                _girlSmr.Add(partName, partGo.AddComponent<SkinnedMeshRenderer>());
                _girlData.Add(partName, new Dictionary<string, SkinnedMeshRenderer>());
            }
            _girlData[partName].Add(names[1], part);    // 存储所有的skm信息到数据里面
        }
    }

    /// <summary>
    /// 换装实现
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">编号</param>
    void ChangeMesh(string part, string num)
    {
        SkinnedMeshRenderer skm = _girlData[part][num];
        List<Transform> bones = new List<Transform>();
        foreach (var trans in skm.bones)
        {
            foreach (var bone in _girlHips)
            {
                if (bone.name == trans.name)
                {
                    bones.Add(bone);
                    break;
                }
            }
        }
        //换装实现
        var curSkm = _girlSmr[part];
        curSkm.bones = bones.ToArray();
        curSkm.materials = skm.materials;
        curSkm.sharedMesh = skm.sharedMesh;
    }

    /// <summary>
    /// 初始化骨架，让它有mesh 材质 骨骼信息
    /// </summary>
    void InitAvatar()
    {
        int length = _girlStr.GetLength(0);
        for (int i = 0; i < length; i++)
        {
            ChangeMesh(_girlStr[i, 0], _girlStr[i, 1]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public Picture picturePrefab;
    public Transform picSpawnPosition;
    public Vector2 StartPosition = new Vector2(-2.15f, 3.62f);

    [HideInInspector]
    public List<Picture> pictureList;

    private Vector2 _offset = new Vector2(1.5f, 1.52f);

    private List<Material> _materialList = new List<Material>();
    private List<string> _texturePathList = new List<string>();
    private Material _firstMaterial;
    private string _firstTexturePath;

    // Start is called before the first frame update
    void Start()
    {
        LoadMaterials();
        SpawnPictureMesh(4, 5, StartPosition, _offset, false);
        MovePicture(4, 5, StartPosition, _offset);
    }

    private void LoadMaterials()
    {
        var materialFilePath = PairsGameSettings.instance.GetMaterialDirectoryName();
        var textureFilePath = PairsGameSettings.instance.GetPuzzleCategoryDirectoryName();
        var pairNumber = (int)PairsGameSettings.instance.GetPairNumber();
        const string matBaseName = "pic";
        var firstMaterialName = "Back";

        for (int index = 1; index <= pairNumber; index++)
        {
            var currentFilePath = materialFilePath + matBaseName + index;
            Material mat = Resources.Load(currentFilePath, typeof(Material)) as Material;
            _materialList.Add(mat);

            var currentTexturePath = textureFilePath + matBaseName + index;
            _texturePathList.Add(currentFilePath);
        }

        _firstTexturePath = textureFilePath + firstMaterialName;
        _firstMaterial = Resources.Load(materialFilePath + firstMaterialName, typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnPictureMesh(int rows, int columns, Vector2 pos, Vector2 offSet, bool scaleDown)
    {
        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                var tempPicture = (Picture)Instantiate(picturePrefab, picSpawnPosition.position, picSpawnPosition.transform.rotation);

                tempPicture.name = tempPicture.name + "c" + col + "r" + row;
                pictureList.Add(tempPicture);
            }
        }

        ApplyTexture();
    }

    private void ApplyTexture()
    {
        var rndMatIndex = Random.Range(0, _materialList.Count);
        var AppliedTimes = new int[_materialList.Count];

        for (int i = 0; i < _materialList.Count; i++)
        {
            AppliedTimes[i] = 0;
        }

        foreach (var o in pictureList)
        {
            var randPrevious = rndMatIndex;
            var counter = 0;
            var forceMat = false;

            while (AppliedTimes[rndMatIndex] >= 2 || ((randPrevious == rndMatIndex) && !forceMat))
            {
                rndMatIndex = Random.Range(0, _materialList.Count);
                counter++;
                if (counter > 100)
                {
                    for (int j = 0; j < _materialList.Count; j++)
                    {
                        if (AppliedTimes[j] < 2)
                        {
                            rndMatIndex = j;
                            forceMat = true;
                        }
                    }

                    if (forceMat == false)
                        return;
                }
            }

            o.SetFirstMaterial(_firstMaterial, _firstTexturePath);
            o.ApplyFirstMaterial();
            o.SetSecondMaterial(_materialList[rndMatIndex], _texturePathList[rndMatIndex]);
            AppliedTimes[rndMatIndex] += 1;
            forceMat = false;
        }
    }

    private void MovePicture(int rows, int columns, Vector2 pos, Vector2 offSet)
    {
        var index = 0;

        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                var targetPosition = new Vector3((pos.x + (offSet.x * row)), (pos.y - (offSet.y * col)), 0.0f);
                StartCoroutine(MoveToPosition(targetPosition, pictureList[index]));
                index++;
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, Picture obj)
    {
        var randomDis = 7;
        while (obj.transform.position != target)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, randomDis * Time.deltaTime);
            yield return 0;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairsGameSettings : MonoBehaviour
{
    private readonly Dictionary<EPuzzleCategories, string> _puzzleCatDirectory = new Dictionary<EPuzzleCategories, string>();
    private int _settings;
    private const int settingsNumber = 2;

    public enum EpairNumber
    {
        NotSet = 0,
        E10Pairs = 10,
        E15Pairs = 15,
        E20Pairs = 20,
    }
    public enum EPuzzleCategories 
    {
        NotSet,
        Fruits,
        Vegetables,
    }

    public struct Settings
    {
        public EpairNumber pairsNumber;
        public EPuzzleCategories puzzleCategory;
    }

    private Settings _gameSettings;

    public static PairsGameSettings instance;

    void Awake()
    {
        if (instance == null) 
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPuzzleDirectory();
        _gameSettings = new Settings();
        ResetGameSettings();
    }

    private void SetPuzzleDirectory() 
    {
        _puzzleCatDirectory.Add(EPuzzleCategories.Fruits, "Fruits");
        _puzzleCatDirectory.Add(EPuzzleCategories.Vegetables, "Vegetables");
    }

    public void SetPairNumber(EpairNumber number) 
    {
        if (_gameSettings.pairsNumber == EpairNumber.NotSet)
            _settings++;
        
        _gameSettings.pairsNumber = number;
    }

    public void SetPuzzleCategories(EPuzzleCategories category) 
    {
        if (_gameSettings.puzzleCategory == EPuzzleCategories.NotSet)
            _settings++;

        _gameSettings.puzzleCategory = category;
    }

    public EpairNumber GetPairNumber() { return _gameSettings.pairsNumber; }
    public EPuzzleCategories GetEPuzzleCategories() { return _gameSettings.puzzleCategory;}

    public void ResetGameSettings() 
    {
        _settings = 0;
        _gameSettings.puzzleCategory = EPuzzleCategories.NotSet;
        _gameSettings.pairsNumber = EpairNumber.NotSet;
    }
    
    public bool AllSetingsReady() 
    {
        return _settings == settingsNumber;
    }

    public string GetMaterialDirectoryName() 
    {
        return "Material/";
    }

    public string GetPuzzleCategoryDirectoryName() 
    {
        if (_puzzleCatDirectory.ContainsKey(_gameSettings.puzzleCategory))
        {
            return "Graphics/PuzzleCat" + _puzzleCatDirectory[_gameSettings.puzzleCategory];
        }
        else
        {
            Debug.LogError("ERROR: CANNOT GET DIRECTORY NAME");
            return "";
        }
    }
}

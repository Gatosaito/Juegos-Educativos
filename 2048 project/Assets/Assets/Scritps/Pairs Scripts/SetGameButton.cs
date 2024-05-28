using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetGameButton : MonoBehaviour
{
    public enum EButtonType 
    {
        NotSet,
        PairNumberButton,
        PuzzleCategoryButton,
    }
    [SerializeField] public EButtonType buttonType = EButtonType.NotSet;
    [HideInInspector] public PairsGameSettings.EpairNumber pairNumber = PairsGameSettings.EpairNumber.NotSet;
    [HideInInspector] public PairsGameSettings.EPuzzleCategories puzzleCategories = PairsGameSettings.EPuzzleCategories.NotSet;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameSceneOption(string gameSceneName) 
    {
        var comp = gameObject.GetComponent<SetGameButton>();

        switch(buttonType) 
        {
            case SetGameButton.EButtonType.PairNumberButton:
                PairsGameSettings.instance.SetPairNumber(comp.pairNumber);
                break;

            case EButtonType.PuzzleCategoryButton:
                PairsGameSettings.instance.SetPuzzleCategories(comp.puzzleCategories); 
                break;
        }

        if (PairsGameSettings.instance.AllSetingsReady())
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}

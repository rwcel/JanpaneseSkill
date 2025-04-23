using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private UIManager _uiManager;

    [SerializeField] private WordSO _wordSO;
    
    private Stack<WordBase> _words;
    
    private void Start()
    {
        // 선택한 데이터에 따라 설정 + 셔플
        _words = ShuffleWords(GetWordSO());
        
        _uiManager.OnStart(_words.Count);
        
        // 시작?
        SetWord();
    }
    
    private List<WordBase> GetWordSO()
    {
        return _wordSO.Words;
    }

    /// <summary>
    /// 데이터 섞기
    /// </summary>
    private Stack<WordBase> ShuffleWords(List<WordBase> words)
    {
        var random = new System.Random();
        var orderWords = new Stack<WordBase>(
            words.OrderBy(_ => random.Next())
            );

        return orderWords;
    }

    private void SetWord()
    {
        var wordBase = GetWordBase();
        
        _uiManager.SetWord(wordBase);
    }

    private WordBase GetWordBase()
    {
        if (_words.Count <= 0)
        {
            Debug.LogError("끝처리 필요");
            return new WordBase();      // null처리 필요
        }
        
        return _words.Pop();
    }
}

using System.Collections.Generic;
using UnityEngine;


// 두 단어 고정으로 할지..
[System.Serializable]
public struct WordBase
{
    public string word1, word2;     // 단어
    public string read1, read2;     // 요미카타
    public string meaning;          // 뜻
    
    public string Word => word1 + word2;
    public string Read => read1 + read2;
}

[CreateAssetMenu(fileName = "WordSO", menuName = "Scriptable Objects/WordSO")]
public class WordSO : ScriptableObject
{
    
    [SerializeField]
    private List<WordBase> words;

    public List<WordBase> Words => words;

}

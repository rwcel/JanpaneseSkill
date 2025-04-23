using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _wordText, _readText, _meanText;
    
    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField] 
    private Button _readButton, _nextButton;

    
    [SerializeField] 
    private TextMeshProUGUI _curIdxText, _maxIdxText;
    
    [SerializeField] 
    private TextMeshProUGUI _wrongCountText;

    private GameObject _goWord, _goRead, _goMean;
    private GameObject _goReadButton, _goNextButton;
    
    private static readonly Color COLOR_MISS = new Color(1f,0.55f,0.58f);
    private static readonly Color COLOR_CORRECT = new Color(0.55f,1f,0.58f);


    private WordBase _curWord;
    private IntReactiveProperty _curIdx = new IntReactiveProperty(0);
    private IntReactiveProperty _maxIdx = new IntReactiveProperty(0);
    private IntReactiveProperty _wrongCount = new IntReactiveProperty(0);

    
    public void OnStart()
    {
        SetData();

        InitData();
    }

    private void SetData()
    {
        _goWord = _wordText.gameObject;
        _goRead = _readText.gameObject;
        _goMean = _meanText.gameObject;

        _goReadButton = _readButton.gameObject;
        _goNextButton =  _nextButton.gameObject;
        
        _readButton.onClick.AddListener(OnReadButtonClick);
        
        _inputField.onEndEdit.AddListener(OnCheckMeaning);

        _maxIdx.Subscribe(value => _maxIdxText.text = $"/ {value}")
            .AddTo(this);
        _curIdx.SubscribeToTextMeshPro(_curIdxText)
            .AddTo(this);
        _wrongCount.SubscribeToTextMeshPro(_wrongCountText)
            .AddTo(this);
    }
    
    public void SetData(int maxCount, UnityAction onClickNextButton)
    {
        _maxIdx.Value = maxCount;
        _curIdx.Value = 0;
        _wrongCount.Value = 0;
        
        _nextButton.onClick.AddListener(onClickNextButton);
    }

    private void InitData()
    {
        ClearData();
    }

    private void ClearData()
    {
        _goWord.SetActive(true);
        _goRead.SetActive(false);
        _goMean.SetActive(false);
        
        _goReadButton.SetActive(true);
        _goNextButton.SetActive(false);
        
        _inputField.text = "";
    }

    private void OnReadButtonClick()
    {
        _goReadButton.SetActive(false);
        
        _goRead.SetActive(true);
    }

    /// <summary>
    /// 데이터 초기화
    /// </summary>
    public void SetWord(WordBase wordBase)
    {
        ClearData();

        _curIdx.Value++;

        _curWord = wordBase;
        
        _wordText.text = wordBase.Word;
        _readText.text = wordBase.Read;
        _meanText.text = wordBase.meaning;
    }


    /// <summary>
    /// 정답확인 (버튼 or inputfield)
    /// 타입에 따라 구분 따로 해야함
    /// </summary>
    private void OnCheckMeaning(string word)
    {
        if (string.IsNullOrEmpty(word))
            return;
        
        Debug.Log($"{word} == {_curWord.meaning}");

        var isCorrect = word.Equals(_curWord.meaning);
        if(!isCorrect)
            _wrongCount.Value++;
        
        var buttonColors = _nextButton.colors; 
        buttonColors.normalColor = isCorrect ? COLOR_CORRECT : COLOR_MISS;
        _nextButton.colors = buttonColors;
        
        _goMean.SetActive(true);
        _goNextButton.SetActive(true);
    }
}

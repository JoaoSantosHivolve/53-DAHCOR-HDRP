using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutChoiceButton : RacketLayoutChoiceElement
{
    private int _AnswerIndex;
    private Color _SelectedColor = Color.black;
    private Color _UnselectedColor = new Color(213f/255f, 213f/255f, 213f/255f);
    private Button _Button;
    private Image _Image;
   
        
    protected override void Initialize()
    {
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(OnClick);
        _Image = GetComponent<Image>();

        if(transform.childCount > 1)
        {
            _PriceText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }

    public void OnClick()
    {
        // Change Button Outline Color
        _Image.color = _SelectedColor;

        // Set other buttons Unselected
        (_Question as RacketLayoutQuestionButtons).ClearOtherSelectedButtons(this);

        // Send condition if any
        (_Question as RacketLayoutQuestionButtons).OnSelectingChoice(_AnswerIndex);

        // Set question answered
        _Question.SetAnswered();

        var answerText = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        (_Question as RacketLayoutQuestionButtons).SetAnswerData(answerText, Price);
    }

    public void SetIndex(int index) => _AnswerIndex = index;

    public void SetUnselected()
    {
        _Image.color = _UnselectedColor;
    }

    //[ContextMenu("Change Font Size")]
    //void ChangeFontSize()
    //{
    //    var rect = GetComponent<RectTransform>().rect;
    //    rect.height = 25;
    //    GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);
    //
    //    GetComponent<Image>().pixelsPerUnitMultiplier = 30;
    //
    //    transform.GetChild(0).GetComponent<TextMeshProUGUI>().enableAutoSizing = false;
    //    transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 15;
    //}
}
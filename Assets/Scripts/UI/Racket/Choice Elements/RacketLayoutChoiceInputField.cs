using TMPro;

public class RacketLayoutChoiceInputField : RacketLayoutChoiceElement
{
    private TMP_InputField _InputField;

    protected override void Initialize()
    {
        _InputField = GetComponent<TMP_InputField>();
        _InputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string arg0)
    {
        if(arg0 != "")
        {
            _Question.SetAnswered();
        }
    }
}
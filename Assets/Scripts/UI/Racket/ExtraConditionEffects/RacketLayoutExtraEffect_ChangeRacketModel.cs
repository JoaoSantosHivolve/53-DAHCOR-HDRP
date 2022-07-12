using UnityEngine;

public class RacketLayoutExtraEffect_ChangeRacketModel : RacketLayoutExtraEffect
{
    [SerializeField] private PartToModify _Part;
    private RacketLayoutQuestionButtons _Question;
    private int AnswerIndex
    {
        get { return _Question.GetSelectedAnswer(); }
    }

    public override void Initialize()
    {
        _Question = GetComponent<RacketLayoutQuestionButtons>();
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        RacketCostumizerController.Instance.ChangeObject(_Part, GetModel());
    }

    private ModelData GetModel()
    {
        switch (_Part)
        {
            case PartToModify.Body:
                return DataLoader.Instance.GetBodyData()[AnswerIndex];
            case PartToModify.Head:
                return DataLoader.Instance.GetHeadData()[AnswerIndex];
            case PartToModify.Grip:
                return DataLoader.Instance.GetGripData()[AnswerIndex];
            case PartToModify.Buttcap:
                return DataLoader.Instance.GetButtcapData()[AnswerIndex];
            default:
                return new ModelData();
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutSummaryController : Singleton<RacketLayoutSummaryController>
{
    // ----- VARIABLES
    #region - BODY VARIABLES
    [Header("- BODY ---------")]
    // base
    public RacketLayoutQuestion bodyLayoutQuestion;
    public TextMeshProUGUI bodyLayoutText;
    public RacketLayoutQuestion bodyCostumizationQuestion;
    public TextMeshProUGUI bodyCostumizationText;
    // colormap
    public RacketLayoutQuestion bodyFinishQuestion;
    public TextMeshProUGUI bodyFinishText;
    public RacketLayoutQuestion bodySelectedColorIcon;
    public TextMeshProUGUI bodySelectedColorText;
    public Image bodySelectedColorImage;
    public RacketLayoutQuestion bodySelectedColorTwoIcon;
    public TextMeshProUGUI bodySelectedColorTwoText;
    public Image bodySelectedColorTwoImage;
    // immaterials
    public RacketLayoutQuestion bodyMaterialOne;
    public RacketLayoutQuestion bodyMaterialIconOne;
    public TextMeshProUGUI bodyMaterialOneText;
    public RacketLayoutQuestion bodyMaterialTwo;
    public RacketLayoutQuestion bodyMaterialIconTwo;
    public TextMeshProUGUI bodyMaterialTwoText;
    #endregion
    #region -- HEAD VARIABLES
    [Header("-- HEAD --------")]
    // base
    public RacketLayoutQuestion headLayoutQuestion;
    public TextMeshProUGUI headLayoutText;
    public RacketLayoutQuestion headCostumizationQuestion;
    public TextMeshProUGUI headCostumizationText;
    // colormap
    public RacketLayoutQuestion headFinishQuestion;
    public TextMeshProUGUI headFinishText;
    public RacketLayoutQuestion headSelectedColorIcon;
    public TextMeshProUGUI headSelectedColorText;
    public Image headSelectedColorImage;
    public RacketLayoutQuestion headSelectedColorTwoIcon;
    public TextMeshProUGUI headSelectedColorTwoText;
    public Image headSelectedColorTwoImage;
    // immaterials
    public RacketLayoutQuestion headMaterialOne;
    public RacketLayoutQuestion headMaterialIconOne;
    public TextMeshProUGUI headMaterialOneText;
    public RacketLayoutQuestion headMaterialTwo;
    public RacketLayoutQuestion headMaterialIconTwo;
    public TextMeshProUGUI headMaterialTwoText;
    #endregion
    #region --- LETTERING VARIABLES
    [Header("--- LETTERING ---")]
    // base
    public RacketLayoutQuestion letteringLayoutQuestion;
    public TextMeshProUGUI letteringLayoutText;
    // colormap
    public RacketLayoutQuestion letteringFinishQuestion;
    public TextMeshProUGUI letteringFinishText;
    public RacketLayoutQuestion letteringSelectedColorIcon;
    public TextMeshProUGUI letteringSelectedColorText;
    public Image letteringSelectedColorImage;
    #endregion
    #region ---- BUMPER VARIABLES
    [Header("----- BUMPER ----")]
    public RacketLayoutQuestion bumperCostumizationQuestion;
    public TextMeshProUGUI bumperCostumizationText;
    // colormap
    public RacketLayoutQuestion bumperFinishQuestion;
    public TextMeshProUGUI bumperFinishText;
    public RacketLayoutQuestion bumperSelectedColorIcon;
    public TextMeshProUGUI bumperSelectedColorText;
    public Image bumperSelectedColorImage;
    // immaterials
    public RacketLayoutQuestion bumperMaterialOne;
    public RacketLayoutQuestion bumperMaterialIconOne;
    public TextMeshProUGUI bumperMaterialOneText;
    #endregion
    #region ----- STRINGS VARIABLES
    [Header("----- STRING ----")]
    public RacketLayoutQuestion stringCostumizationQuestion;
    public TextMeshProUGUI stringCostumizationText;
    public RacketLayoutQuestion stringSelectedColorIcon;
    public TextMeshProUGUI stringSelectedColorText;
    public Image stringSelectedColorImage;
    #endregion
    #region ------ HANDLE VARIABLES
    [Header("------- HANDLE --")]
    // base
    public RacketLayoutQuestion handleLayoutQuestion;
    public TextMeshProUGUI handleLayoutText;
    // colors
    public RacketLayoutQuestion handleSelectedColorIcon;
    public TextMeshProUGUI handleSelectedColorText;
    public Image handleSelectedColorImage;
    public RacketLayoutQuestion handleSelectedColorTwoIcon;
    public TextMeshProUGUI handleSelectedColorTwoText;
    public Image handleSelectedColorTwoImage;
    // immaterials
    public RacketLayoutQuestion handleMaterialOne;
    public RacketLayoutQuestion handleMaterialIconOne;
    public TextMeshProUGUI handleMaterialOneText;
    // buttcap ype
    public RacketLayoutQuestion buttcapTypeQuestion;
    public TextMeshProUGUI buttcapTypeText;
    #endregion
    #region ------- AUTOGRAPH VARIABLES
    [Header("------- AUTOGRAPH --")]
    // base
    public RacketLayoutQuestion autographSignatureQuestion;
    public TextMeshProUGUI autographSignatureText;
    // buttcap ype
    public RacketLayoutQuestion autographShowFlagQuestion;
    public RacketLayoutQuestion autographFlagDropdownQuestion;
    public TextMeshProUGUI autographShowFlagText;
    #endregion
    #region -------- TOTAL VARIABLES
    [Header("-------- TOTAL --")]
    public TextMeshProUGUI totalText;
    #endregion

    private void Start()
    {
        UpdateSummary();
    }

    public void UpdateSummary()
    {
        #region ---------- BODY ----------
        // Layout
        if (IsQuestionValid(bodyLayoutQuestion))
        {
            bodyLayoutText.text = "\t<b>Layout:</b> " + GetAnswerInfo(bodyLayoutQuestion);
        }
        else bodyLayoutText.text = "";
        // Costumization
        if (IsQuestionValid(bodyCostumizationQuestion))
        {
            bodyCostumizationText.text = "\t<b>Costumization:</b> " + GetAnswerInfo(bodyCostumizationQuestion);
        }
        else bodyCostumizationText.text = "";
        // Finish
        if (IsQuestionValid(bodyFinishQuestion))
        {
            bodyFinishText.text = "\t<b>Finish:</b> " + GetAnswerInfo(bodyFinishQuestion);
        }
        else bodyFinishText.text = "";
        // Color 1
        if (IsQuestionValid(bodySelectedColorIcon))
        {
            bodySelectedColorImage.gameObject.SetActive(true);
            bodySelectedColorText.text = "\t<b>Selected Color:</b> ";
            bodySelectedColorImage.color = (bodySelectedColorIcon as RacketLayoutQuestionIcon).GetSelectedColor();
        }
        else
        {
            bodySelectedColorImage.gameObject.SetActive(false);
            bodySelectedColorText.text = "";
        }
        // Color 2
        if (IsQuestionValid(bodySelectedColorTwoIcon))
        {
            bodySelectedColorTwoImage.gameObject.SetActive(true);
            bodySelectedColorTwoText.text = "\t<b>Selected Color:</b> ";
            bodySelectedColorTwoImage.color = (bodySelectedColorTwoIcon as RacketLayoutQuestionIcon).GetSelectedColor();
        }
        else
        {
            bodySelectedColorTwoImage.gameObject.SetActive(false);
            bodySelectedColorTwoText.text = "";
        }
        // Material 1
        if (IsQuestionValid(bodyMaterialOne) && IsQuestionValid(bodyMaterialIconOne))
        {
            bodyMaterialOneText.text = "\t<b>Material 1:</b> " + GetAnswerInfo(bodyMaterialOne, bodyMaterialIconOne);
        }
        else bodyMaterialOneText.text = "";
        // Material 2
        if (IsQuestionValid(bodyMaterialTwo) && IsQuestionValid(bodyMaterialIconTwo))
        {
            bodyMaterialTwoText.text = "\t<b>Material 2:</b> " + GetAnswerInfo(bodyMaterialTwo, bodyMaterialIconTwo);
        }
        else bodyMaterialTwoText.text = "";
        #endregion
        #region ---------- HEAD ----------
        // Layout
        if (IsQuestionValid(headLayoutQuestion))
        {
            headLayoutText.text = "\t<b>Layout:</b> " + GetAnswerInfo(headLayoutQuestion);
        }
        else headLayoutText.text = "";
        // Costumization
        if (IsQuestionValid(headCostumizationQuestion))
        {
            headCostumizationText.text = "\t<b>Costumization:</b> " + GetAnswerInfo(headCostumizationQuestion);
        }
        else headCostumizationText.text = "";
        // Finish
        if (IsQuestionValid(headFinishQuestion))
        {
            headFinishText.text = "\t<b>Finish:</b> " + GetAnswerInfo(headFinishQuestion);
        }
        else headFinishText.text = "";
        // Color 1
        if (IsQuestionValid(headSelectedColorIcon))
        {
            headSelectedColorImage.gameObject.SetActive(true);
            headSelectedColorText.text = "\t<b>Selected Color:</b> ";
            headSelectedColorImage.color = (headSelectedColorIcon as RacketLayoutQuestionIcon).GetSelectedColor();
        }
        else
        {
            headSelectedColorImage.gameObject.SetActive(false);
            headSelectedColorText.text = "";
        }
        // Color 2
        if (IsQuestionValid(headSelectedColorTwoIcon))
        {
            headSelectedColorTwoImage.gameObject.SetActive(true);
            headSelectedColorTwoText.text = "\t<b>Selected Color:</b> ";
            headSelectedColorTwoImage.color = (headSelectedColorTwoIcon as RacketLayoutQuestionIcon).GetSelectedColor();
        }
        else
        {
            headSelectedColorTwoImage.gameObject.SetActive(false);
            headSelectedColorTwoText.text = "";
        }
        // Material 1
        if (IsQuestionValid(headMaterialOne) && IsQuestionValid(headMaterialIconOne))
        {
            headMaterialOneText.text = "\t<b>Material 1:</b> " + GetAnswerInfo(headMaterialOne, headMaterialIconOne);
        }
        else headMaterialOneText.text = "";
        // Material 2
        if (IsQuestionValid(headMaterialTwo) && IsQuestionValid(headMaterialIconTwo))
        {
            headMaterialTwoText.text = "\t<b>Material 2:</b> " + GetAnswerInfo(headMaterialTwo, headMaterialIconTwo);
        }
        else headMaterialTwoText.text = "";
        #endregion
        #region ---------- LETTERING ----------
        // Model
        if (IsQuestionValid(letteringLayoutQuestion))
        {
            letteringLayoutText.text = "\t<b>Model:</b> " + GetAnswerInfo(letteringLayoutQuestion);
        }
        else letteringLayoutText.text = "";
        // Colormap
        if (IsQuestionValid(letteringFinishQuestion))
        {
            letteringFinishText.text = "\t<b>Costumization:</b> " + GetAnswerInfo(letteringFinishQuestion);
        }
        else letteringFinishText.text = "";
        // Color 1
        if (IsQuestionValid(letteringSelectedColorIcon))
        {
            letteringSelectedColorImage.gameObject.SetActive(true);
            letteringSelectedColorText.text = "\t<b>Selected Color:</b> ";
            letteringSelectedColorImage.color = (letteringSelectedColorIcon as RacketLayoutQuestionIcon).GetSelectedColor();
        }
        else
        {
            letteringSelectedColorImage.gameObject.SetActive(false);
            letteringSelectedColorText.text = "";
        }
        #endregion
        #region ---------- BUMPER ----------
        // Costumization
        if (IsQuestionValid(bumperCostumizationQuestion))
        {
            bumperCostumizationText.text = "\t<b>Costumization:</b> " + GetAnswerInfo(bumperCostumizationQuestion);
        }
        else bumperCostumizationText.text = "";
        // Finish
        if (IsQuestionValid(bumperFinishQuestion))
        {
            bumperFinishText.text = "\t<b>Finish:</b> " + GetAnswerInfo(bumperFinishQuestion);
        }
        else bumperFinishText.text = "";
        // Color 1
        if (IsQuestionValid(bumperSelectedColorIcon))
        {
            bumperSelectedColorImage.gameObject.SetActive(true);
            bumperSelectedColorText.text = "\t<b>Selected Color:</b> ";
            bumperSelectedColorImage.color = (bumperSelectedColorIcon as RacketLayoutQuestionIcon).GetSelectedColor();
        }
        else
        {
            bumperSelectedColorImage.gameObject.SetActive(false);
            bumperSelectedColorText.text = "";
        }
        // Material 1
        if (IsQuestionValid(bumperMaterialOne) && IsQuestionValid(bumperMaterialIconOne))
        {
            bumperMaterialOneText.text = "\t<b>Material:</b> " + GetAnswerInfo(bumperMaterialOne, bumperMaterialIconOne);
        }
        else bumperMaterialOneText.text = "";
        #endregion
        #region ---------- STRING ----------
        // Costumization
        if (IsQuestionValid(stringCostumizationQuestion))
        {
            stringCostumizationText.text = "\t<b>Costumization:</b> " + GetAnswerInfo(stringCostumizationQuestion);
        }
        else stringCostumizationText.text = "";
        // Color 1
        if (IsQuestionValid(stringSelectedColorIcon))
        {
            stringSelectedColorImage.gameObject.SetActive(true);
            stringSelectedColorText.text = "\t<b>Selected Color:</b> ";
            stringSelectedColorImage.color = (stringSelectedColorIcon as RacketLayoutQuestionIcon).GetSelectedColor();
        }
        else
        {
            stringSelectedColorImage.gameObject.SetActive(false);
            stringSelectedColorText.text = "";
        }
        #endregion
        #region ---------- HANDLE ----------
        // Layout
        if (IsQuestionValid(handleLayoutQuestion))
        {
            handleLayoutText.text = "\t<b>Grip type:</b> " + GetAnswerInfo(handleLayoutQuestion);
        }
        else handleLayoutText.text = "";
        // Color 1
        if (IsQuestionValid(handleSelectedColorIcon))
        {
            handleSelectedColorImage.gameObject.SetActive(true);
            handleSelectedColorText.text = "\t<b>Selected Color:</b> ";
            handleSelectedColorImage.color = (handleSelectedColorIcon as RacketLayoutQuestionIcon).GetSelectedColor();
        }
        else
        {
            handleSelectedColorImage.gameObject.SetActive(false);
            handleSelectedColorText.text = "";
        }
        // Material 1
        if (IsQuestionValid(handleMaterialOne) && IsQuestionValid(handleMaterialIconOne))
        {
            handleMaterialOneText.text = "\t<b>Material:</b> " + GetAnswerInfo(handleMaterialOne, handleMaterialIconOne);
        }
        else handleMaterialOneText.text = "";
        // Buttcap Type
        if (IsQuestionValid(buttcapTypeQuestion))
        {
            buttcapTypeText.text = "\t<b>Buttcap Type:</b> " + GetAnswerInfo(buttcapTypeQuestion);
        }
        else buttcapTypeText.text = "";
        // Color 1
        if (IsQuestionValid(handleSelectedColorTwoIcon))
        {
            handleSelectedColorTwoImage.gameObject.SetActive(true);
            handleSelectedColorTwoText.text = "\t<b>Selected Color:</b> ";
            handleSelectedColorTwoImage.color = (handleSelectedColorTwoIcon as RacketLayoutQuestionIcon).GetSelectedColor();
        }
        else
        {
            handleSelectedColorTwoImage.gameObject.SetActive(false);
            handleSelectedColorTwoText.text = "";
        }
        #endregion
        #region ---------- AUTOGRAPH ----------
        // Signature
        if (IsQuestionValid(autographSignatureQuestion))
        {
            autographSignatureText.text = "\t<b>Grip type:</b> " + GetAnswerInfo(autographSignatureQuestion);
        }
        else autographSignatureText.text = "";
        // Flag
        if (IsQuestionValid(autographShowFlagQuestion))
        {
            autographShowFlagText.text = "\t<b>Show flag:</b> " + GetAnswerInfo(autographShowFlagQuestion);
        }
        else autographShowFlagText.text = "";
        #endregion

        totalText.text = "Total: $" + GetTotalPrice().ToString();
    }

    private string GetAnswerInfo(RacketLayoutQuestion question)
    {
        return question.GetAnswer() + GetPrice(question);
    }
    private string GetAnswerInfo(RacketLayoutQuestion question, RacketLayoutQuestion question2)
    {
        return question.GetAnswer() + " - " + question2.GetAnswer() + GetPrice(question2);
    }
    private string GetPrice(RacketLayoutQuestion question)
    {
        var cost = question.GetCost();
        return cost == 0 ? "" : ("<color=#C0C0C0> +$" + cost.ToString() + "</color>");
    }
    private bool IsQuestionValid(RacketLayoutQuestion question)
    {
        return (question.IsAnswered && question.gameObject.activeInHierarchy);
    }
    private int GetTotalPrice()
    {
        var questions = GameObject.FindObjectsOfType<RacketLayoutQuestion>();
        var total = 0;
        foreach (var item in questions)
        {
            if (IsQuestionValid(item))
                total += item.GetCost();
        }
        return total;
    }
}

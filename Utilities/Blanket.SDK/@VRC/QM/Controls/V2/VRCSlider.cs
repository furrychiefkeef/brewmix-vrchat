using BlanketSDK.Core;
using Brewchat.Game.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM.Elements.Controls.V2;
internal class VRCSliderOperator : IQuickMenuElements
{
    public enum SliderType
    {
        None,
        Int,
        Float,
        Percentage,
        PercentageNoRestrict,
        Meters,
        Kilometers,
        MetersPerSecond,
        KilometersPerHour,
        MetersPerMinute,
        MetersPerHour,
        MetersPerDay,
        MetersPerWeek,
        MetersPerMonth,
        MetersPerYear,
        MetersPerDecade,
        MetersPerCentury,
        MetersPerMillenium,
        MetersPerMillenia,
        MetersPerBillion,
        MetersPerTrillion,

    }
    public VRCSliderOperator(Transform parent, string name, SliderType type, float defaultval, float min, float max, Action<float> val, Action<bool> func = null, bool wholeNumbers = false, bool state = false)
    {
        if (!QM() || !QMParent() || !QMMenus.QMSubmenus.Menu_QM_GeneralSettings()) throw new($"element has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(QMControls.OperatorSlider(), parent);

        obj.name = $"{Guid.NewGuid().ToString()}-<{nameof(QM)}";
       
        #region REQUIRED_COMPONENTS

        MonoBehaviour2PublicSiLoSiOb_s_nObGa_iCaUnique? __0 = obj.GetComponent<MonoBehaviour2PublicSiLoSiOb_s_nObGa_iCaUnique>(); // SliderOperator
        #endregion

        #region LOGIC_REQUIRED_API

        #region  Toggle
        RadioButton? __1 = __0._toggleSwitch;
        VRCToggleHandle? _0 = __1.gameObject.transform.parent.GetComponent<VRCToggleHandle>();
        __1.field_Private_Single_0 = 27.54f; // Fix it going off element
        __1.field_Private_Single_1 = 95.4124f; // Fix it going off element
        Toggle.ToggleEvent? LISTENER = _0.onValueChanged;

        __1.Method_Public_Void_Boolean_PDM_1(false);
        _0.isOn = state;
        
        LISTENER.AddListener(new System.Action<bool>(x =>
        {
            __1.Method_Public_Void_Boolean_PDM_1(x);
            func(x);
        }));
        _0._controlName = name;
        _0.field_Private_Boolean_0 = true;
        _0._sendAnalytics = false;
        #endregion

        #region Slider
        __0._numberText.prop_String_0 = OnSelectedType(type, defaultval);
        var Slider = __0._slider;
        
        Slider.value = defaultval;
        Slider.minValue = min;
        Slider.maxValue = max;
        Slider.wholeNumbers = wholeNumbers;
        Slider.onValueChanged.AddListener(new System.Action<float>(x =>
        {
            __0._numberText.prop_String_0 = OnSelectedType(type, x);
            val(x);
        }));
        #endregion



        #endregion

        #region TEXT_COMPONENT

        TextMeshProUGUIEx? Title = __0._title;
        Title.text = name;
        Title.richText = true;

        #endregion
    }
    private string OnSelectedType(SliderType type, float x)
    {
        switch (type)
        {
            case SliderType.None:
                return $"{x}";
            case SliderType.Int:
                return $"{(int)x}";
            case SliderType.Float:
                return $"{x}f";
            case SliderType.Percentage:
                return $"{x * 100}%";
            case SliderType.PercentageNoRestrict:
                return  $"{x}%";
            case SliderType.Meters:
                return  $"{Math.Round(x, 2)}m";
            case SliderType.Kilometers:
                return $"{Math.Round(x / 1000, 2)}km";
            default:
                return $"{x}";
                // case SliderType.MetersPerSecond:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x, 2)}m/s";
                //     break;
                // case SliderType.KilometersPerHour:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 1000, 2)}km/h";
                //     break;
                // case SliderType.MetersPerMinute:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60, 2)}m/min";
                //     break;
                // case SliderType.MetersPerHour:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60, 2)}m/h";
                //     break;
                // case SliderType.MetersPerDay:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24, 2)}m/day";
                //     break;
                // case SliderType.MetersPerWeek:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24 / 7, 2)}m/wk";
                //     break;
                // case SliderType.MetersPerMonth:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24 / 30, 2)}m/mo";
                //     break;
                // case SliderType.MetersPerYear:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24 / 365, 2)}m/yr";
                //     break;
                // case SliderType.MetersPerDecade:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24 / 365 / 10, 2)}m/dec";
                //     break;
                // case SliderType.MetersPerCentury:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24 / 365 / 100, 2)}m/cen";
                //     break;
                // case SliderType.MetersPerMillenium:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24 / 365 / 1000, 2)}m/mil";
                //     break;
                // case SliderType.MetersPerMillenia:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24 / 365 / 10000, 2)}m/mil";
                //     break;
                // case SliderType.MetersPerBillion:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24 / 365 / 1000000, 2)}m/bil";
                //     break;
                // case SliderType.MetersPerTrillion:
                //     __0._numberText.prop_String_0 = $"{Math.Round(x / 60 / 60 / 24 / 365 / 1000000000, 2)}m/tril";
                //     break;
        }
    }
}
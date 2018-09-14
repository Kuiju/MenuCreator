using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonInternal 
{

    public string ButtonName = "Unknow";      public Action ButtonClickAction = null;      public Action ButtonHoverBeginAction = null;      public Action ButtonHoverEndAction = null;      public List<ButtonInternal> SubButtonInternalList = new List<ButtonInternal>();      public ButtonInternal(string buttonName)     {         ButtonName = buttonName;         ButtonClickAction = () => { Debug.Log(" button " + buttonName + " clicked ..."); };     }      public ButtonInternal BindSub(ButtonInternal subButtonInternal)     {         SubButtonInternalList.Add(subButtonInternal);         return this;     } 









}

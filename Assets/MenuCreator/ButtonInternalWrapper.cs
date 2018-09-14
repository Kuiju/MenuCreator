using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ButtonInternalWrapper
{

    public ButtonInternal Internal = null;      public GameObject ButtonRootGo = null;

    public GameObject ButtonText = null;

    public RectTransform  Rect;      public Button ButtonComponent = null;      public ButtonInternalWrapper ParentWrapper = null;      public List<ButtonInternalWrapper> SubWrapperList = new List<ButtonInternalWrapper>();      public bool bSubOpened { get; private set; }      public void OpenAllSubWrapper()     {         bSubOpened = true;          foreach (var sub in SubWrapperList)         {             if (sub.ButtonRootGo != null)                 sub.ButtonRootGo.SetActive(true);         }     }      public void CloseAllSubWrapper(bool bRecursive = true, bool bForce = false)     {         if (!bSubOpened && !bForce)             return;         bSubOpened = false;          foreach (var sub in SubWrapperList)         {             if (sub.ButtonRootGo != null)                 sub.ButtonRootGo.SetActive(false);             if (bRecursive)                 sub.CloseAllSubWrapper(bRecursive);         }     } 
    public float GetButtonShrinkedByMaxTextLength()
    {
        ButtonRootGo.GetComponent<RectTransform>().sizeDelta = new Vector2(ButtonText.GetComponent<Text>().preferredWidth, 30.0f);
        float distance = 0;
        float length = 0;
        foreach (var sub in SubWrapperList )
        {
            length =(length >sub.ButtonText .GetComponent <Text >().preferredWidth )?length: sub.ButtonText.GetComponent<Text>().preferredWidth;
            
        }

        foreach (var sub in SubWrapperList )
        {
            sub.ButtonRootGo.GetComponent<RectTransform>().sizeDelta = new Vector2(length, 30.0f);
        }
        distance = (ButtonText.GetComponent<Text>().preferredWidth + length) / 2.0f;
        return distance ;
    }




}

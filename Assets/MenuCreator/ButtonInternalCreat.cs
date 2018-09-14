using System; using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.EventSystems; using UnityEngine.UI;

public class ButtonInternalCreat : MonoBehaviour {     [SerializeField]     public ButtonInternalConfig CreateConfig = null;      public EventOnButton eventOnButton = null;      public RectTransform CreateRootTransform1 = null;      public RectTransform CreateRootTransform2 = null;      public GameObject monitor;      public static  bool IsOnUI = false;      ButtonInternalWrapper rootWrapper1;     private void Start()     {         TestCreateButton();      }      public void Update()
    {
        if(Input .GetMouseButtonDown(0)&& !monitor .GetComponent <RealTimeMonitor >().IsOnUI )         {             rootWrapper1.CloseAllSubWrapper(true);         }         
    }      public void TestCreateButton()     {         ButtonInternal testButtonInternal = CreateTestButtonInternal();          rootWrapper1 = CreateButtonByButtonInternal(testButtonInternal, CreateRootTransform1);         ButtonInternalWrapper rootWrapper2 = CreateButtonByButtonInternal(testButtonInternal, CreateRootTransform2);              }      #region Create Test Button Internal      private ButtonInternal CreateTestButtonInternal()     {         ButtonInternal Root = new ButtonInternal("Root_" + GetRandomInt());          ButtonInternal Sub_1 = new ButtonInternal("Sub_1_" + GetRandomInt());         ButtonInternal Sub_1_1 = new ButtonInternal("Sub_1_1_" + GetRandomInt());         ButtonInternal Sub_1_2 = new ButtonInternal("Sub_1_2_" + GetRandomInt());         ButtonInternal Sub_1_3 = new ButtonInternal("Sub_1_3_" + GetRandomInt());         ButtonInternal Sub_1_4 = new ButtonInternal("Sub_1_4_" + GetRandomInt());         Sub_1.BindSub(Sub_1_1).BindSub(Sub_1_2).BindSub(Sub_1_3).BindSub(Sub_1_4);          ButtonInternal Sub_2 = new ButtonInternal("Sub_2_" + GetRandomInt());         ButtonInternal Sub_2_1 = new ButtonInternal("Sub_2_1_" + GetRandomInt());         ButtonInternal Sub_2_2 = new ButtonInternal("Sub_2_2_" + GetRandomInt());         ButtonInternal Sub_2_3 = new ButtonInternal("Sub_2_3_" + GetRandomInt());         Sub_2.BindSub(Sub_2_1).BindSub(Sub_2_2).BindSub(Sub_2_3);          ButtonInternal Sub_2_2_1 = new ButtonInternal("Sub_2_2_1_" + GetRandomInt());         ButtonInternal Sub_2_2_2 = new ButtonInternal("Sub_2_2_2_" + GetRandomInt());         ButtonInternal Sub_2_2_3 = new ButtonInternal("Sub_2_2_3_" + GetRandomInt());         Sub_2_2.BindSub(Sub_2_2_1).BindSub(Sub_2_2_2).BindSub(Sub_2_2_3);          ButtonInternal Sub_2_2_3_1 = new ButtonInternal("Sub_2_2_3_1_" + GetRandomInt());         ButtonInternal Sub_2_2_3_2 = new ButtonInternal("Sub_2_2_3_2_" + GetRandomInt());         ButtonInternal Sub_2_2_3_3 = new ButtonInternal("Sub_2_2_3_3_" + GetRandomInt());         Sub_2_2_3.BindSub(Sub_2_2_3_1).BindSub(Sub_2_2_3_2).BindSub(Sub_2_2_3_3);          ButtonInternal Sub_3 = new ButtonInternal("Sub_3_" + GetRandomInt());         ButtonInternal Sub_3_1 = new ButtonInternal("Sub_3_1_" + GetRandomInt());         ButtonInternal Sub_3_2 = new ButtonInternal("Sub_3_2_" + GetRandomInt());         Sub_3.BindSub(Sub_3_1).BindSub(Sub_3_2);          Root.BindSub(Sub_1).BindSub(Sub_2).BindSub(Sub_3);         return Root;     }      private int GetRandomInt()     {         return UnityEngine.Random.Range(10000, 99999);     }      #endregion      #region Create Button By Button Internal      private ButtonInternalWrapper CreateButtonByButtonInternal(ButtonInternal buttonInternal, RectTransform createRootTransform)     {         if (buttonInternal == null || createRootTransform == null)             return null;          // 1.create button internal self                  GameObject buttonRootGo= ButtonGenerate(createRootTransform ,buttonInternal .ButtonName ,CreateConfig .NormalButtonTexture ,CreateConfig .SelectButtonTexture );         EventOnButton btnAdvance = buttonRootGo.AddComponent<EventOnButton>();         buttonRootGo.GetComponent<RectTransform>().sizeDelta = new Vector2(CreateConfig.DefaultButtonWidth, CreateConfig.DefaultButtonHeight);          GameObject buttonText= TextGenerate (buttonRootGo ,buttonInternal .ButtonName ,CreateConfig .DefaultFont ,Color .black );          ButtonInternalWrapper wrapper = new ButtonInternalWrapper();         wrapper.Internal = buttonInternal;         wrapper.ButtonRootGo = buttonRootGo;         wrapper.ButtonText = buttonText;         wrapper.ButtonComponent = buttonRootGo .GetComponent <Button>();
        wrapper.Rect = createRootTransform ;


        buttonRootGo.GetComponent<Button>().onClick.AddListener(() =>         {             if (!CreateConfig.TriggerWithClick)             {
                buttonInternal.ButtonClickAction();

                if (wrapper.ParentWrapper == null)
                {
                    if (wrapper.bSubOpened)
                        wrapper.CloseAllSubWrapper(true);
                    else
                        wrapper.OpenAllSubWrapper();
                }              }               else if(CreateConfig.TriggerWithClick)             {

                wrapper.CloseAllSubWrapper(true);

                if (wrapper.ParentWrapper == null)                 {                      if (wrapper.bSubOpened)                         wrapper.CloseAllSubWrapper(true);                     else                         wrapper.OpenAllSubWrapper();                 } 


                wrapper.OpenAllSubWrapper();


                if (wrapper.ParentWrapper != null)                 {                     foreach (var brotherWrapper in wrapper.ParentWrapper.SubWrapperList)                     {                         if (brotherWrapper != wrapper)                             brotherWrapper.CloseAllSubWrapper(true);                     }                 }

            }                     });          btnAdvance .OnHoverBeginAction = () =>         {             if (!CreateConfig.TriggerWithClick)             {
                wrapper.CloseAllSubWrapper(true);

                if (wrapper.ParentWrapper == null)
                    return;

                wrapper.OpenAllSubWrapper();

                if (wrapper.ParentWrapper != null)
                {
                    foreach (var brotherWrapper in wrapper.ParentWrapper.SubWrapperList)
                    {
                        if (brotherWrapper != wrapper)
                            brotherWrapper.CloseAllSubWrapper(true);
                    }
                }

            }              else if (!CreateConfig.TriggerWithClick)             {              }


        };          btnAdvance  .OnHoverEndAction = () =>         {          };            // 2.create sub         for (int i = 0; i < buttonInternal.SubButtonInternalList.Count; i++)         {
            RectTransform subRect = RectTransformGenerate(createRootTransform,i ).GetComponent<RectTransform>();              ButtonInternalWrapper subWrapper = CreateButtonByButtonInternal(buttonInternal.SubButtonInternalList[i], subRect);              subWrapper.Rect = subRect;             subWrapper.ParentWrapper = wrapper;             wrapper.SubWrapperList.Add(subWrapper);         }          //3.shrink As Text Length         if(CreateConfig .bButtonShrinkAsTextWidth )         {             float len= wrapper.GetButtonShrinkedByMaxTextLength();             for (int i = 0; i < buttonInternal.SubButtonInternalList.Count; i++)             {                 wrapper .SubWrapperList [i].Rect.GetComponent<RectTransform>().anchoredPosition = new Vector2(len, -CreateConfig.DefaultButtonHeight * i);             } 

        }             wrapper.CloseAllSubWrapper(true, true);         return wrapper;     }

    #endregion 

    #region Method To Generate Gameobject 
    public GameObject TextGenerate(GameObject   textParent, String textName, Font textFont, Color textColor)     {         GameObject thistext = new GameObject(textName);         thistext.transform.SetParent(textParent.transform);         thistext.transform.localPosition = Vector3.zero;         thistext.transform.localRotation = Quaternion.identity;         thistext.transform.localScale = Vector3.one;          thistext.AddComponent<Text>();         thistext.AddComponent<RectTransform>();         thistext.AddComponent<ContentSizeFitter>();          thistext.GetComponent<Text>().font = textFont;         thistext.GetComponent<Text>().color = textColor;         thistext.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;         thistext.GetComponent<Text>().text = textName;         thistext.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;         thistext.GetComponent<Text>().verticalOverflow  = VerticalWrapMode.Overflow;         thistext.GetComponent<Text>().fontSize = 18;         thistext.GetComponent<Text>().raycastTarget = false;         thistext.name = textName;         return thistext;     }           public GameObject ButtonGenerate(RectTransform  btnParent, string btnName, Sprite normaltexture, Sprite selecttexture)     {          GameObject btn = new GameObject(btnName);         btn.transform.SetParent(btnParent.transform, false);
        btn .transform.localPosition = Vector3.zero;         btn .transform.localRotation = Quaternion.identity;         btn.transform.localScale = Vector3.one;          btn.AddComponent<RectTransform>();         btn.AddComponent<Button>();         btn.AddComponent<Image>();         btn.AddComponent<EventOnButton >();          btn.name = btnName;         btn.GetComponent<Button>().transition = Selectable.Transition.SpriteSwap;         btn.GetComponent<Button>().targetGraphic = btn.GetComponent<Image>();         btn.GetComponent<Image>().sprite = normaltexture;         SpriteState btnImage = new SpriteState();         btnImage.disabledSprite = normaltexture;         btnImage.highlightedSprite = selecttexture;         btnImage.pressedSprite = normaltexture;         btn.GetComponent<Button>().spriteState = btnImage;         return btn;      }      public GameObject   RectTransformGenerate(RectTransform parent,int btnNum)     {         GameObject  rectTransform=new GameObject ("subRect");         rectTransform.transform.SetParent(parent);         rectTransform.transform.localPosition = Vector3.zero;         rectTransform.transform.localRotation = Quaternion.identity;         rectTransform.transform.localScale = Vector3.one;         rectTransform.AddComponent<RectTransform>();         rectTransform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;                 rectTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(CreateConfig.DefaultButtonWidth, -CreateConfig.DefaultButtonHeight * btnNum);         return rectTransform;     }         #endregion
}

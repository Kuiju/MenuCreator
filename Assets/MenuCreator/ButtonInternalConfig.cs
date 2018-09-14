using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class ButtonInternalConfig 
{

    private static Font _BuiltinFont = null;     public static Font BuiltinFont     {         get         {             if (_BuiltinFont)                 _BuiltinFont = Resources.GetBuiltinResource<Font>("Arial.ttf");             return _BuiltinFont;         }     }      public Font DefaultFont = BuiltinFont;      public Color DefaultColor = Color.black;      public float DefaultButtonWidth = 160.0f;      public float DefaultButtonHeight = 30.0f;      public bool bButtonShrinkAsTextWidth = true;      public bool TriggerWithClick = false;      public Sprite NormalButtonTexture;      public Sprite SelectButtonTexture;      public Sprite BackgroundTexture;      public int ShrinkPading = 0; 
}

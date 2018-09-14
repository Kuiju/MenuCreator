using System; using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.EventSystems;

public class EventOnButton : MonoBehaviour,IPointerExitHandler ,IPointerEnterHandler  
{

    public Action OnHoverBeginAction = null;     public Action OnHoverEndAction = null;
    public GameObject Monitoring;
    public void Start()
    {
         Monitoring = GameObject.Find("RealTimeMonitor");
    }     public void OnPointerEnter(PointerEventData eventData)     {         if (OnHoverBeginAction != null)             OnHoverBeginAction();
        Monitoring.GetComponent <RealTimeMonitor >().IsOnUI = true;     }      public void OnPointerExit(PointerEventData eventData)     {         if (OnHoverEndAction != null)             OnHoverEndAction();
        Monitoring.GetComponent<RealTimeMonitor>().IsOnUI = false;     }

    
}

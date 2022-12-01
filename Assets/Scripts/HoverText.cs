 using UnityEngine;
 using System.Collections;
 using UnityEngine.EventSystems;
 using UnityEngine.UI;
 using TMPro;
 
 [RequireComponent( typeof( Button ) )]
 public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
 {
     [SerializeField] private TextMeshProUGUI txt;
     [SerializeField] private Button btn;
     [SerializeField] private GameObject normalImage;
     [SerializeField] private GameObject hoverImage;
 
     public Color normalColor;
     public Color pressedColor;
     public Color highlightedColor;
 
     private ButtonStatus lastButtonStatus = ButtonStatus.Normal;
     private bool isHighlightDesired = false;
     private bool isPressedDesired = false;

     void Start() {
        hoverImage.SetActive(false);
     }
 
     void Update()
     {
        ButtonStatus desiredButtonStatus = ButtonStatus.Normal;
        if ( isHighlightDesired )
            desiredButtonStatus = ButtonStatus.Highlighted;
        if ( isPressedDesired )
            desiredButtonStatus = ButtonStatus.Pressed;
 
         if ( desiredButtonStatus != this.lastButtonStatus )
         {
             this.lastButtonStatus = desiredButtonStatus;
             switch ( this.lastButtonStatus )
             {
                 case ButtonStatus.Normal:
                     txt.color = normalColor;
                     hoverImage.SetActive(false);
                     normalImage.SetActive(true);
                     break;
                 case ButtonStatus.Pressed:
                     txt.color = pressedColor;
                     break;
                 case ButtonStatus.Highlighted:
                     txt.color = highlightedColor;
                     hoverImage.SetActive(true);
                     normalImage.SetActive(false);
                     break;
             }
         }
     }
 
     public void OnPointerEnter( PointerEventData eventData )
     {
         isHighlightDesired = true;
     }
 
     public void OnPointerDown( PointerEventData eventData )
     {
         isPressedDesired = true;
     }
 
     public void OnPointerUp( PointerEventData eventData )
     {
         isPressedDesired = false;
     }
 
     public void OnPointerExit( PointerEventData eventData )
     {
         isHighlightDesired = false;
     }
 
     public enum ButtonStatus
     {
         Normal,
         Highlighted,
         Pressed
     }
 }
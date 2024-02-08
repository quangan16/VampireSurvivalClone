using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour,IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        AudioSystem.Instance.PlayOnce(SoundEfx.BUTTON_ENTER);
    }

    public void OnPointerClick(PointerEventData data)
    {
        AudioSystem.Instance.PlayOnce(SoundEfx.BUTTON_PRESS);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        audioManager.Play("Menu");
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        transform.GetChild(1).gameObject.SetActive(false);
        audioManager.Play("Select");
    }
}

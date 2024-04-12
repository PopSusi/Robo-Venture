using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    private enum TextType {Tutorial, Objective};
    [SerializeField][Tooltip("Type of box - effects where text is displayed.")] TextType myType;
    [SerializeField]
    [Tooltip("Blank")] string text;
    private UIManager uiMan;
    bool activated;

    // Start is called before the first frame update
    void Start()
    {
        uiMan = UIManager.instance;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && activated == false)
        {
            activated = true;
            DisplayText(activated);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && activated == true)
        {
            activated = false;
            DisplayText(activated);
            Destroy(gameObject);
        }
    }
    private void DisplayText(bool active)
    {
        if(myType == TextType.Tutorial)
        {
            uiMan.TutorialText.transform.parent.gameObject.SetActive(active);
            uiMan.TutorialText.text = text;
        }
        else
        {
            uiMan.ObjectiveText.gameObject.SetActive(active);
            uiMan.ObjectiveText.text = text;
        }
    }
}

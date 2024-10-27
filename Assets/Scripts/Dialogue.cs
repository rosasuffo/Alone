using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerSubject;
    [SerializeField] Phone phone;

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (phone.GetCurrentState() == phone.dialogue1State || phone.GetCurrentState() == phone.dialogue2State))
        {
            if (textComponent.text == lines[index] && index == lines.Length - 1)
            {
                StopAllCoroutines();
                textComponent.text = "";
                phone.dialogue = false;
            }
            else if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void OnNotify(PlayerActions action)
    {
        if ((action == PlayerActions.Phone1Interaction || action == PlayerActions.Phone2Interaction) && phone.GetCurrentState() == phone.ringingState)
        {
            StartDialogue();
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }
}
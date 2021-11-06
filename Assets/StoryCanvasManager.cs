using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryCanvasManager : MonoBehaviour
{
    [SerializeField] private string[] sentences;
    private int current_idx = 0;
    public TMP_Text text;
    public GameObject container;
    public CameraCinematicMovement cameraCinematicMovement;
    private Animator animator;

    [Header("COMPONENTS")]
    public Transform casaBruja;
    public GameObject bruja;
    // Start is called before the first frame update
    private void OnEnable()
    {
        
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        text.text = sentences[0];
    }

    public void OnContinueButton()
    {
        current_idx++;

        if (current_idx >= sentences.Length)
        {
            //container.SetActive(false);
            SceneManager.LoadScene(1);
            return;
        }

        if (current_idx == 2)
        {
            cameraCinematicMovement.GoToPosition(casaBruja.position, 0.5f);
            bruja.GetComponent<Animator>().SetTrigger("Fly");
            animator.SetTrigger("blink");
            //cameraCinematicMovement.desiredPosition = casaBruja.position;
            StartCoroutine(TurnOnContainer());
            return;
        }
        animator.SetTrigger("blink");
        StartCoroutine(UpdateText());
    }

    IEnumerator UpdateText()
    {
        yield return new WaitForSeconds(0.3f);
        text.text = sentences[current_idx];
    }
    IEnumerator TurnOnContainer()
    {
        yield return new WaitForSeconds(0.5f);
        container.SetActive(false);
        yield return new WaitForSeconds(2.7f);
        animator.SetTrigger("blink");
        yield return new WaitForSeconds(0.3f);
        container.SetActive(true);
        current_idx = 2;
        text.text = sentences[current_idx];
        //OnContinueButton();
    }
}

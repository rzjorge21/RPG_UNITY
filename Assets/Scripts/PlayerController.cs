using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public MenuManager menuManager;
    public float Health { get; set; }

    private float movementX;
    private float movementY;

    public bool CanMove { get; set; }

    public Animator animator;
    public TextMeshProUGUI nameTag;
    public TMP_InputField nameTagInput;

    private void Awake()
    {
        Health = 3f;
        CanMove = true;
        // Nos vamos a inscribir como observadores de MenuManager (OnMenuOpenEvent)
        menuManager.OnMenuOpenEvent += MenuManager_OnMenuOpenEvent;
        // Nos vamos a inscribir como observadores de MenuManager (OnMenuCloseEvent)
        menuManager.OnMenuCloseEvent += MenuManager_OnMenuCloseEvent;
    }

    private void Start()
    {
        
        if (PlayerPrefs.GetString("NAME_TAG") != "")
        {
            nameTag.SetText(PlayerPrefs.GetString("NAME_TAG"));
            nameTagInput.text = PlayerPrefs.GetString("NAME_TAG");
        }
        else
        {
            nameTag.SetText("Peter C.");
        }
    }

    private void MenuManager_OnMenuCloseEvent(object sender, System.EventArgs e)
    {
        CanMove = true;
    }

    private void MenuManager_OnMenuOpenEvent(object sender, System.EventArgs e)
    {
        CanMove = false;
    }

    private void Update()
    {
        if (CanMove)
        {
            movementX = Input.GetAxisRaw("Horizontal");
            movementY = Input.GetAxisRaw("Vertical");

            //Animation
            animator.SetFloat("Horizontal", movementX);
            animator.SetFloat("Vertical", movementY);
            Vector2 movement = new Vector2(movementX, movementY);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            transform.position += (new Vector3(movementX, movementY)) * Time.deltaTime * speed ;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.gameObject.name);
        if (collision.transform.CompareTag("Character"))
        {
            Debug.Log("Se inicia el dialogo");
            CanMove = false;
            if(collision.gameObject.name == "Pusheen") {
                 DialogueManager.Instance.StartDialogue(0);
            } else if (collision.gameObject.name == "Rondero") {
                 DialogueManager.Instance.StartDialogue(1);
            } else if (collision.gameObject.name == "Mama") {
                 DialogueManager.Instance.StartDialogue(2);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Continue"))
        {
            Debug.Log("Continuara");
            CanMove = false;
            GameObject.Find("/ContinueCanvas").transform.GetChild(0).gameObject.SetActive(true);
            
        }
    }

    public void Heal(float amount, bool boost)
    {
        if (boost) Health = 3f;
        Health += amount;
        Debug.Log($"Player Health: {Health}");
    }

    public void UpdateNameTag()
    {
        PlayerPrefs.SetString("NAME_TAG", nameTagInput.text);
        nameTagInput.text = PlayerPrefs.GetString("NAME_TAG");
        nameTag.SetText(PlayerPrefs.GetString("NAME_TAG"));
    }
}

using UnityEngine;
using UnityEngine.UI;


public class SelectionArrow : MonoBehaviour
{
    [SerializeField]private RectTransform[] options;
    [SerializeField]private AudioClip changeSound; //The sound we play when we move the arrow up/down 
    [SerializeField] private AudioClip interactSound; //The sound we play when an option is selected 
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //change position 
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //interact with options
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();

    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if(_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if(currentPosition < 0) 
            currentPosition = options.Length - 1;
        else if(currentPosition > options.Length - 1)
            currentPosition = 0;


        //Assign the Y position of the current option to the arrow (basically moving it up/down )
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}

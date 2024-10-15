/**
 * A singleton class to allow point-and-click movement of the marble.
 * 
 * It publishes a TargetSelected event which is invoked whenever a new target is selected.
 * 
 * Author: Malcolm Ryan
 * Version: 1.0
 * For Unity Version: 2022.3
 */

using UnityEngine;
using UnityEngine.InputSystem;

// note this has to run earlier than other classes which subscribe to the TargetSelected event
[DefaultExecutionOrder(-100)]
public class UIManager : MonoBehaviour
{
#region UI Elements
    [SerializeField] private Transform crosshair;
    [SerializeField] private Transform target;
#endregion 

#region Singleton
    static private UIManager instance;
    static public UIManager Instance
    {
        get { return instance; }
    }
#endregion 

#region Actions
    private Actions actions;
    private InputAction mouseAction;
    private InputAction deltaAction;
    private InputAction selectAction;
#endregion

#region Events
    public delegate void TargetSelectedEventHandler(Vector3 worldPosition);
    public event TargetSelectedEventHandler TargetSelected;
    #endregion

    Plane m_Plane;

    #region Init & Destroy
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one UIManager in the scene.");
        }

        instance = this;

        actions = new Actions();
        mouseAction = actions.mouse.position;
        deltaAction = actions.mouse.delta;
        selectAction = actions.mouse.select;

        Cursor.visible = false;
        target.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        actions.mouse.Enable();
    }

    void OnDisable()
    {
        actions.mouse.Disable();
    }
    private void Start()
    {
        m_Plane = new Plane(Vector3.up, new Vector3(0, 0.1f, 0));
    }
    #endregion Init

    #region Update
    void Update()
    {
        MoveCrosshair();
        SelectTarget();
    }

    private void MoveCrosshair() 
    {
        // TASK 1
        //Vector2 mousePos = mouseAction.ReadValue<Vector2>();
        //float mouseX = mousePos.x;
        //float mouseY = mousePos.y;
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 10));
        //Vector3 crosshairPos;
        //crosshairPos = new Vector3(worldPos.x, 0.1f, worldPos.z);
        //crosshair.position = crosshairPos;

        // TASK 4
        //Vector2 mousePos = mouseAction.ReadValue<Vector2>();
        //Ray ray = Camera.main.ScreenPointToRay(mousePos);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        //float enter = 0.0f;
        //if (m_Plane.Raycast(ray, out enter))
        //{
        //    Vector3 hitPoint = ray.GetPoint(enter);
        //    //Debug.Log(hitPoint);
        //    crosshair.position = hitPoint;
        //}

        // TASK 5
        //Vector2 deltaPos = deltaAction.ReadValue<Vector2>();
        //float mouseX = deltaPos.x;
        //float mouseY = deltaPos.y;

        //crosshair.position += new Vector3(mouseX / 2, 0, mouseY / 2);
    }

    private void SelectTarget()
    {
        if (selectAction.WasPerformedThisFrame())
        {
            // set the target position and invoke 
            target.gameObject.SetActive(true);
            target.position = crosshair.position;     
            TargetSelected?.Invoke(target.position);       
        }
    }

#endregion Update

}

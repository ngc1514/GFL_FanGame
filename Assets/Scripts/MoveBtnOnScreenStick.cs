using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine;


/// <summary>
/// A stick control displayed on screen and moved around by touch or other pointer
/// input.
/// </summary>
public class MoveBtnOnScreenStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public MyCineProvider myCineProvider;

    public void OnPointerUp(PointerEventData eventData)
    {
        ((RectTransform)transform).anchoredPosition = m_StartPos;
        SendValueToControl(Vector2.zero);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log($"Event data: {eventData}");

        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out m_PointerDownPos);
    }


    // FIXME: where is "not OnDrag"
    public void OnDrag(PointerEventData eventData)
    {
        // TODO: set true only when dragging the MoveBtn
        //Debug.Log("Dragging");
        //Util.isDraggingMoveBtn = true;

        myCineProvider.SetDraggingMoveBtnTrue();

        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);
        var delta = position - m_PointerDownPos;

        delta = Vector2.ClampMagnitude(delta, movementRange);
        ((RectTransform)transform).anchoredPosition = m_StartPos + (Vector3)delta;

        var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
        SendValueToControl(newPos);
    }


    private void Start()
    {
        m_StartPos = ((RectTransform)transform).anchoredPosition;
    }

    public float movementRange
    {
        get => m_MovementRange;
        set => m_MovementRange = value;
    }

    [FormerlySerializedAs("movementRange")]
    [SerializeField]
    private float m_MovementRange = 50;

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;

    private Vector3 m_StartPos;
    private Vector2 m_PointerDownPos;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

}
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = .025f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    public Transform spawnPos;
    public GameObject spawnee;
    GameObject clone;


    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        if (!_isPressed && GetValue() + threshold >= 1) Pressed();
        if (_isPressed && GetValue() - threshold <= 0) Released();
    }
    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone) value = 0;
        return Mathf.Clamp(value, -1f, 1f);
    }
    private void Pressed()
    {
        foreach (Hand hand in GameObject.FindObjectsOfType<Hand>())
        {

            GameObject gameObject = hand.currentAttachedObject;
            if (gameObject!=null)
            {
                if (gameObject.Equals(clone))
                {
                    hand.DetachObject(gameObject, false);
                }
            }
            Destroy(clone);
        }
        clone = (GameObject)Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
        _isPressed = true;
    }

    private void Released()
    {
        _isPressed = false;
    }
}

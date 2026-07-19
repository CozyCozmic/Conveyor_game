using UnityEngine;
using UnityEngine.InputSystem;

public class PickupController : MonoBehaviour
{
    public Camera playerCamera;
    public Transform holdPoint;
    public float pickupRange = 3f;
    public float throwForce = 15f;
    public LayerMask pickupLayer;

    public float positionSpring = 500f;
    public float positionDamper = 50f;
    public float maxHoldForce = 250f;
    public float maxHoldDistance = 1.5f;

    private ConfigurableJoint joint;
    private Rigidbody heldBody;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (heldBody == null)
                TryPickup();
            else
                Throw();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame && heldBody != null)
        {
            Drop();
        }

        if (heldBody != null)
        {
            float dist = Vector3.Distance(heldBody.position, holdPoint.position);
            if (dist > maxHoldDistance)
            {
                Drop();
            }
        }
    }

    void TryPickup()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                heldBody = rb;
                heldBody.transform.position = holdPoint.position;
                heldBody.transform.rotation = holdPoint.rotation;
                heldBody.linearVelocity = Vector3.zero;
                heldBody.angularVelocity = Vector3.zero;

                joint = holdPoint.gameObject.AddComponent<ConfigurableJoint>();
                joint.connectedBody = heldBody;

                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;

                joint.angularXMotion = ConfigurableJointMotion.Locked;
                joint.angularYMotion = ConfigurableJointMotion.Locked;
                joint.angularZMotion = ConfigurableJointMotion.Locked;

                JointDrive drive = new JointDrive
                {
                    positionSpring = positionSpring,
                    positionDamper = positionDamper,
                    maximumForce = maxHoldForce
                };

                joint.xDrive = drive;
                joint.yDrive = drive;
                joint.zDrive = drive;
            }
        }
    }

    void Throw()
    {
        if (joint != null)
            Destroy(joint);

        heldBody.linearVelocity = Vector3.zero;
        heldBody.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);

        heldBody = null;
        joint = null;
    }

    void Drop()
    {
        if (joint != null)
            Destroy(joint);

        heldBody.linearVelocity = Vector3.zero;
        heldBody.angularVelocity = Vector3.zero;

        heldBody = null;
        joint = null;
    }
}
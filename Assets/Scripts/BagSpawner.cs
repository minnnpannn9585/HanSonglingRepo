using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class BagSpawner : MonoBehaviour
{
    [Header("Core")]
    public GameObject popcornPrefab;      // Prefab (recommended: contains Rigidbody + XRGrabInteractable)
    public Transform spawnPoint;          // Where to instantiate inside the bag
    public float spawnCooldown = 0.3f;

    [Header("Input")]
    public InputActionReference gripActionRef; // assign RightHand Grip action in inspector

    XRDirectInteractor currentHand;
    GameObject heldPopcorn;
    bool isOnCooldown;

    void Awake()
    {
        if (gripActionRef?.action != null && !gripActionRef.action.enabled)
            gripActionRef.action.Enable();
    }

    void OnTriggerEnter(Collider other)
    {
        var direct = other.GetComponent<XRDirectInteractor>();
        if (direct == null) return;
        currentHand = direct;
    }

    void OnTriggerExit(Collider other)
    {
        var direct = other.GetComponent<XRDirectInteractor>();
        if (direct == null) return;
        if (direct == currentHand) currentHand = null;
    }

    void Update()
    {
        if (currentHand == null || isOnCooldown || popcornPrefab == null || spawnPoint == null || gripActionRef == null || gripActionRef.action == null)
            return;

        bool pressed = gripActionRef.action.IsPressed();

        if (pressed && heldPopcorn == null)
        {
            SpawnAndAttach();
        }
        else if (!pressed && heldPopcorn != null)
        {
            ReleaseFromHand();
            StartCooldown();
        }
    }

    void SpawnAndAttach()
    {
        var go = Instantiate(popcornPrefab, spawnPoint.position, spawnPoint.rotation);
        if (go == null) return;

        // if prefab has XRGrabInteractable, disable it while parented
        var grab = go.GetComponent<XRGrabInteractable>();
        if (grab != null) grab.enabled = false;

        var rb = go.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        // parent to the interactor's attach transform if available, otherwise to interactor transform
        Transform attach = currentHand.attachTransform != null ? currentHand.attachTransform : currentHand.transform;
        go.transform.SetParent(attach, worldPositionStays: false);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;

        heldPopcorn = go;
    }

    void ReleaseFromHand()
    {
        if (heldPopcorn == null) return;

        // unparent and re-enable physics/interactable
        heldPopcorn.transform.SetParent(null, true);

        var rb = heldPopcorn.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        var grab = heldPopcorn.GetComponent<XRGrabInteractable>();
        if (grab != null) grab.enabled = true;

        heldPopcorn = null;
    }

    void StartCooldown()
    {
        isOnCooldown = true;
        Invoke(nameof(ResetCooldown), spawnCooldown);
    }

    void ResetCooldown() => isOnCooldown = false;
}
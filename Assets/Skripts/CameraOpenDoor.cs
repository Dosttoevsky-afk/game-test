
using UnityEngine;

namespace CameraDoorScript
{
    public class CameraOpenDoor : MonoBehaviour
    {
        public float DistanceOpen = 3f;
        public GameObject text;
        public KeyInventory inventory;
        public Camera playerCamera;

        void Start()
        {
            if (inventory == null)
                inventory = FindFirstObjectByType<KeyInventory>();

            if (playerCamera == null)
                playerCamera = GetComponentInChildren<Camera>();

            if (playerCamera == null)
                playerCamera = Camera.main;

            if (text != null)
                text.SetActive(false);

            Debug.Log("Система взаимодействия запущена");
        }

        void Update()
        {
            if (playerCamera == null)
                return;

            Ray ray = new Ray(
                playerCamera.transform.position,
                playerCamera.transform.forward
            );

            if (Physics.Raycast(
                ray,
                out RaycastHit hit,
                DistanceOpen,
                Physics.DefaultRaycastLayers,
                QueryTriggerInteraction.Collide))
            {
                KeyPickup key =
                    hit.collider.GetComponentInParent<KeyPickup>();

                if (key != null)
                {
                    if (text != null)
                        text.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                        key.PickUp(inventory);

                    return;
                }

                DoorScript.SlidingKeyDoor slidingDoor =
                    hit.collider.GetComponentInParent<
                        DoorScript.SlidingKeyDoor>();

                if (slidingDoor != null)
                {
                    if (text != null)
                        text.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                        slidingDoor.Interact(inventory);

                    return;
                }

                DoorScript.Door door =
                    hit.collider.GetComponentInParent<
                        DoorScript.Door>();

                if (door != null)
                {
                    if (text != null)
                        text.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                        door.TryOpenDoor(inventory);

                    return;
                }
            }

            if (text != null)
                text.SetActive(false);
        }
    }
}

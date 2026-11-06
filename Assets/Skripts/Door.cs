
using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour
    {
        public bool open;
        public bool requiresKey = true;
        public bool unlocked = false;

        public KeyType requiredKeyType = KeyType.RedKey;

        public float smooth = 1.0f;

        float DoorOpenAngle = -90.0f;
        float DoorCloseAngle = 0.0f;

        public AudioSource asource;
        public AudioClip openDoor, closeDoor;

        void Start()
        {
            asource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (open)
            {
                var target = Quaternion.Euler(0, DoorOpenAngle, 0);

                transform.localRotation = Quaternion.Slerp(
                    transform.localRotation,
                    target,
                    Time.deltaTime * 5 * smooth
                );
            }
            else
            {
                var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);

                transform.localRotation = Quaternion.Slerp(
                    transform.localRotation,
                    target1,
                    Time.deltaTime * 5 * smooth
                );
            }
        }

        public void OpenDoor()
        {
            open = !open;

            asource.clip = open ? openDoor : closeDoor;

            if (asource.clip != null)
                asource.Play();
        }

        
        public void TryOpenDoor(KeyInventory inventory)
        {
    // Если дверь уже разблокирована
    // или ей не нужен ключ
            if (unlocked || !requiresKey)
            {
                OpenDoor();
                return;
            }

            // Проверяем наличие подходящего ключа
            if (inventory != null &&
            inventory.HasKey(requiredKeyType))
            {
                unlocked = true;
                OpenDoor();

                Debug.Log("Дверь открыта ключом: " + requiredKeyType);
            }
            else
            {
                Debug.Log("Нужен ключ: " + requiredKeyType);
            }
        }

    }
}

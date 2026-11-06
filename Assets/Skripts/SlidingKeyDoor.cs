
using UnityEngine;

namespace DoorScript
{
    public class SlidingKeyDoor : MonoBehaviour
    {
        [Header("Створки двери")]
        public Transform leftDoor;
        public Transform rightDoor;

        [Header("Настройки движения")]
        public float openDistance = 1.5f;
        public float speed = 2f;

        [Header("Тип ключа")]
        public KeyType requiredKeyType = KeyType.BlueKey;

        [Header("Состояние двери")]
        public bool isOpen = false;
        public bool unlocked = false;

        private Vector3 leftClosedPosition;
        private Vector3 rightClosedPosition;

        private Vector3 leftOpenPosition;
        private Vector3 rightOpenPosition;

        void Start()
        {
            if (leftDoor == null || rightDoor == null)
            {
                Debug.LogError(
                    "Назначь левую и правую створки двери!"
                );

                enabled = false;
                return;
            }

            leftClosedPosition = leftDoor.localPosition;
            rightClosedPosition = rightDoor.localPosition;

            leftOpenPosition = leftClosedPosition
                + Vector3.left * openDistance;

            rightOpenPosition = rightClosedPosition
                + Vector3.right * openDistance;
        }

        void Update()
        {
            Vector3 leftTarget = isOpen
                ? leftOpenPosition
                : leftClosedPosition;

            Vector3 rightTarget = isOpen
                ? rightOpenPosition
                : rightClosedPosition;

            leftDoor.localPosition = Vector3.MoveTowards(
                leftDoor.localPosition,
                leftTarget,
                Time.deltaTime * speed
            );

            rightDoor.localPosition = Vector3.MoveTowards(
                rightDoor.localPosition,
                rightTarget,
                Time.deltaTime * speed
            );
        }

        
        public void Interact(KeyInventory inventory)
        {
            // Если дверь уже разблокирована,
            // можно свободно открывать и закрывать её
            if (unlocked)
            {
                isOpen = !isOpen;
                return;
            }

            // Проверяем наличие подходящего ключа
            if (inventory != null &&
                inventory.HasKey(requiredKeyType))
            {
                unlocked = true;
                isOpen = true;

                Debug.Log("Раздвижная дверь открыта ключом: "
                    + requiredKeyType);
            }
            else
            {
                Debug.Log("Для этой двери нужен ключ: "
                    + requiredKeyType);
            }
        }

    }
}

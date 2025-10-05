using CSC;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WhackAndBlite
{
    public class LightControl : MonoBehaviour
    {
        [SerializeField] private InputActionReference ChangeScaleAction, RotationAction;
        [SerializeField] private float ControlStep = 1f;
        [SerializeField] private float SmoothSpeed = 5f;
        [SerializeField] private float RotationSmoothSpeed = 10f;
        [SerializeField] private float RotationSensitivity = 1f;
        [SerializeField] private Vector2 MinScale, MaxScale;
        [SerializeField] private Camera mainCamera;

        private float targetScaleX;
        private float targetScaleY;

        private float TargetAngle; 

        private void Start()
        {
            Cursor.visible = false;
            if(mainCamera == null) mainCamera = Camera.main;

            targetScaleX = transform.localScale.x;
            targetScaleY = transform.localScale.y;

            GameState.Instance.StateChanged += (state) =>
            {
                if(state == CSC.State.Defeat)
                {
                    Destroy(this);
                }
            };
        }

        private void FixedUpdate()
        {
            HandleScale();
            Rotate();
        }

        private void HandleScale()
        {
            float scroll = ChangeScaleAction.action.ReadValue<float>();

            if(scroll != 0)
            {
                targetScaleX += scroll * ControlStep;
                targetScaleY -= scroll * ControlStep;
            }

            targetScaleX = Mathf.Clamp(targetScaleX, MinScale.x, MaxScale.x);
            targetScaleY = Mathf.Clamp(targetScaleY, MinScale.y, MaxScale.y);

            Vector3 currentScale = transform.localScale;
            currentScale.x = Mathf.Lerp(currentScale.x, targetScaleX, SmoothSpeed * PlayerInventory.SpeedUpgrade * Time.deltaTime);
            currentScale.y = Mathf.Lerp(currentScale.y, targetScaleY, SmoothSpeed * PlayerInventory.SpeedUpgrade * Time.deltaTime);

            transform.localScale = currentScale;
        }

        private void Rotate()
        {
            float currentRotation = RotationAction.action.ReadValue<float>();

            // Влияние движения мыши по X на угол (можно изменить на Y или комбинировать)
            TargetAngle += currentRotation * RotationSensitivity;

            // Плавное изменение угла
            float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, TargetAngle, RotationSmoothSpeed * PlayerInventory.SpeedUpgrade);
            transform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);
        }
    }
}

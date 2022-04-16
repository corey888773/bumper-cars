using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Czolgi {
    public class BaseJoystick : MonoBehaviour {
        protected float _horizontal, _vertical;

        [SerializeField] protected Transform _pad, _border;
        [SerializeField] protected float _speed;

        private Camera _camera;
        private Resolution _res;

        private Vector3 _padStartPosition, _screenPoint, _newPosition;
        private Vector2 _joystickPos;
        private Touch _closestTouch;

        private float _maxTouchDifferene;
        private float _joystickRadius;

        protected void Start() {
            _camera = Camera.main;
            _joystickPos = _camera.WorldToScreenPoint(transform.position);
            _padStartPosition = _pad.localPosition;
            _joystickRadius = _border.GetComponent<SpriteRenderer>().size.x / 2;
            UpdateResolution();
        }

        protected void Update() {
            if (Application.platform == RuntimePlatform.WindowsEditor)
                CheckResolution();

            if (Input.touchCount > 0) {
                _closestTouch = GetClosestTouch();

                if (_closestTouch.phase == TouchPhase.Moved && Vector2.Distance(_joystickPos, _closestTouch.position) < _maxTouchDifferene * 2) {
                    SetHorizontalAndVertical(_closestTouch.position, _joystickPos);
                    _newPosition = _padStartPosition + new Vector3(_joystickRadius * _horizontal, _joystickRadius * _vertical, 0).normalized;
                }
                if (_closestTouch.phase == TouchPhase.Ended) {
                    (_vertical, _horizontal) = (0, 0);
                    _newPosition = _padStartPosition + new Vector3(_joystickRadius * _horizontal, _joystickRadius * _vertical, 0).normalized;
                }
            }
            _pad.localPosition = Vector3.Lerp(_pad.localPosition, _newPosition, _speed * Time.deltaTime);
        }

        private void CheckResolution() {
            if (_res.width != Screen.width || _res.height != Screen.height) {
                Debug.Log(_camera);
                _screenPoint = _camera.WorldToScreenPoint(transform.position);
                UpdateResolution();
            }
        }

        private void SetHorizontalAndVertical(Vector2 touchPos, Vector2 screenJoysticPos) {
            var xDifference = touchPos.x - screenJoysticPos.x;
            _horizontal = xDifference / _maxTouchDifferene;
            if (_horizontal > 1) _horizontal = 1;
            if (_horizontal < -1) _horizontal = -1;

            var yDifference = _closestTouch.position.y - _screenPoint.y;
            _vertical = yDifference / _maxTouchDifferene;
            if (_vertical > 1) _vertical = 1;
            if (_vertical < -1) _vertical = -1;
        }

        private void UpdateResolution() {
            _res = Screen.currentResolution;
            _screenPoint = _camera.WorldToScreenPoint(transform.position);
            _maxTouchDifferene = _camera.WorldToScreenPoint(transform.position + new Vector3(_joystickRadius, 0, 0)).x - _screenPoint.x;
        }

        private Touch GetClosestTouch() {
            Touch touch = Input.GetTouch(0);
            for (int i = 1; i < Input.touchCount; i++)
                if (Vector2.Distance(touch.position, _joystickPos) < Vector2.Distance(Input.GetTouch(i).position, _joystickPos))
                    touch = Input.GetTouch(i);
            return touch;
        }
    }
}
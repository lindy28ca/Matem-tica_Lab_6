﻿using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mathematics.Week6
{
    public class PlaneController : MonoBehaviour
    {
        [Header("Controls Properties")]
        [SerializeField] private float pitchPlane;
        [SerializeField] private float pitchGain = 1f;
        [SerializeField] private MinMax pitchTreshHold;
        [SerializeField] private float rollPlane;
        [SerializeField] private float rollhGain = 1f;
        [SerializeField] private MinMax rollTreshHold;

        [Header("Rotation Data")]
        [SerializeField] private Quaternion qx = Quaternion.identity; //<0,,0,0,1>
        [SerializeField] private Quaternion qy = Quaternion.identity; //<0,,0,0,1>
        [SerializeField] private Quaternion qz = Quaternion.identity; //<0,,0,0,1>

        [SerializeField] private Quaternion r = Quaternion.identity; //<0,,0,0,1>
        private float anguloSen;
        private float anguloCos;
        public GameObject perdiste;
        protected float _pitchDirection = 0f;
        protected float _rollDirection = 0f;
        public int vida;
        public TextoVida textodelavida;
        bool confirmar = false;

        
        private void FixedUpdate()
        {
            if (_verticalDirection == 0 && _horizontalDirection == 0)
            {
                confirmar = true;
            }
            else
            {
                confirmar=false;
            }
            pitchPlane += _pitchDirection * pitchGain;

            pitchPlane = Mathf.Clamp(pitchPlane, pitchTreshHold.MinValue, pitchTreshHold.MaxValue);

            rollPlane += _rollDirection * rollhGain;

            rollPlane = Mathf.Clamp(rollPlane, rollTreshHold.MinValue, rollTreshHold.MaxValue);

            //rotacion z -> x -> y
            anguloSen = Mathf.Sin(Mathf.Deg2Rad * rollPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * rollPlane * 0.5f);
            qz.Set(0, 0, anguloSen, anguloCos);

            anguloSen = Mathf.Sin(Mathf.Deg2Rad * pitchPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * pitchPlane * 0.5f);
            qx.Set(anguloSen, 0, 0, anguloCos);

            /*anguloSen = Mathf.Sin(Mathf.Deg2Rad * rollPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * rollPlane * 0.5f);
            qy.Set(0, anguloSen, 0, anguloCos);*/

            //multiplicación y -> x -> z
            r = qy * qx * qz;
            UpdatePosition();
            transform.rotation = r;
        }
        //Pitch -> X Axis
        public void RotatePitch(InputAction.CallbackContext context)
        {
            _pitchDirection = context.ReadValue<float>();
        }

        //Roll -> Z Axis
        public void RotateRoll(InputAction.CallbackContext context)
        {
            _rollDirection = context.ReadValue<float>();
        }
        private float _verticalDirection = 0f;
        private float _horizontalDirection = 0f;
        [SerializeField] private float velocitySpeed = 5f;

        private Rigidbody _myRB;

        private void Start()
        {
            _myRB = GetComponent<Rigidbody>();
            textodelavida.Imprimir(vida);
        }

        public void TranslateVertical(InputAction.CallbackContext context)
        {
            _verticalDirection = context.ReadValue<float>();
        }

        public void TranslateHorizontal(InputAction.CallbackContext context)
        {
            _horizontalDirection = context.ReadValue<float>();
        }
        private void Update()
        {
            if (vida <=0)
            {
                perdiste.SetActive(true
                    );
            }
        }
        private void UpdatePosition()
        {
            if (confirmar == true)
            {
                _myRB.velocity = Vector3.zero;
                r = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.fixedDeltaTime * 2f);
            }
            else
            {
                _myRB.velocity = new Vector3(_horizontalDirection * velocitySpeed, -_verticalDirection * velocitySpeed, 0f);

            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "enemigo")
            {
                --vida;
                textodelavida.Imprimir(vida);
            }
        }
    }
    [System.Serializable]
    public struct MinMax
    {
        public float MinValue;
        public float MaxValue;
    }
}

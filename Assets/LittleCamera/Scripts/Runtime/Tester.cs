using System;
using LittleCamera.Utils;
using UnityEngine;

namespace LittleCamera.Runtime
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] public Curve _testCurve;

        private void OnDrawGizmos()
        {
            _testCurve.DrawGizmos(Color.red, Color.magenta, transform.localToWorldMatrix);
        }
    }
}

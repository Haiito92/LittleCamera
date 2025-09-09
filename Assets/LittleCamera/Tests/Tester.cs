using System;
using LittleCamera.Utils;
using UnityEngine;

namespace LittleCamera.Tests
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] private Curve _testCurve;

        private void OnDrawGizmos()
        {
            if(_testCurve == null) return;
            _testCurve.DrawGizmos(Color.magenta, transform.localToWorldMatrix);
        }
    }
}

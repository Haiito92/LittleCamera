using System;
using LittleCamera.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleCamera.Runtime
{
    public class Tester : MonoBehaviour
    {
        [field:SerializeField] public Curve TestCurve { get; private set; }

        private void OnDrawGizmos()
        {
            if(TestCurve == null) return;
            TestCurve.DrawGizmos(Color.red, Color.magenta, transform.localToWorldMatrix);
        }
    }
}

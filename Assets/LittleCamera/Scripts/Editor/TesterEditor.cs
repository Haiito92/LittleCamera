using System;
using LittleCamera.Runtime;
using UnityEditor;
using UnityEngine;

namespace LittleCamera.Editor
{
    
    [CustomEditor(typeof(Tester))]
    public class TesterEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            Tester tester = target as Tester;
            if(tester == null) return;

            foreach (var VARIABLE in tester.TestCurve.)
            {
                // DrawHandle(tester, ref tester._testCurve.A);
                // DrawHandle(tester, ref tester._testCurve.B);
                // DrawHandle(tester, ref tester._testCurve.C);
                // DrawHandle(tester, ref tester._testCurve.D);
            }
            
        }

        private void DrawHandle(Tester tester, ref Vector3 point)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(tester.transform.localToWorldMatrix.MultiplyPoint(point), Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(tester, "Curve Change");
                point = tester.transform.worldToLocalMatrix.MultiplyPoint(newPos);
            }
        }
    }
}

/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oculus.Interaction
{
    /// <summary>
    /// A Transformer that rotates the target about an axis, given two grab points.
    /// Updates apply relative rotational changes, relative to the angle change between the two
    /// grab points each frame.
    /// The axis is defined by a pivot transform: a world position and up vector.
    /// </summary>
    public class TwoGrabRotateTransformerTwoAxes : MonoBehaviour, ITransformer
    {
        public enum Axis
        {
            Right = 0,
            Up = 1,
            Forward = 2
        }

        [SerializeField, Optional]
        private Transform _pivotTransform = null;

        private Transform PivotTransform =>
            _pivotTransform != null ? _pivotTransform : _grabbable.Transform;

        [SerializeField]
        public Axis _rotationAxis1 = Axis.Up;
        public Axis _rotationAxis2 = Axis.Right;

        private IGrabbable _grabbable;

        // vector from the hand at the first grab point to the hand on the second grab point,
        // projected onto the plane of the rotation.
        private Vector3 _previousHandsVectorOnPlane1;
        private Vector3 _previousHandsVectorOnPlane2;

        public void Initialize(IGrabbable grabbable)
        {
            _grabbable = grabbable;
        }

        public void BeginTransform()
        {
            Vector3 rotationAxis1 = CalculateRotationAxisInWorldSpace(1);
            Vector3 rotationAxis2 = CalculateRotationAxisInWorldSpace(2);
            _previousHandsVectorOnPlane1 = CalculateHandsVectorOnPlane(rotationAxis1);
            _previousHandsVectorOnPlane2 = CalculateHandsVectorOnPlane(rotationAxis2);
        }

        public void UpdateTransform()
        {
            Vector3 rotationAxis1 = CalculateRotationAxisInWorldSpace(1);
            Vector3 rotationAxis2 = CalculateRotationAxisInWorldSpace(2);
            Vector3 handsVector1 = CalculateHandsVectorOnPlane(rotationAxis1);
            Vector3 handsVector2 = CalculateHandsVectorOnPlane(rotationAxis2);
            float angleDelta1 =
                Vector3.SignedAngle(_previousHandsVectorOnPlane1, handsVector1, rotationAxis1);
            float angleDelta2 =
                Vector3.SignedAngle(_previousHandsVectorOnPlane2, handsVector2, rotationAxis2);

            // Apply this angle rotation about the axis to our transform
            _grabbable.Transform.RotateAround(PivotTransform.position, rotationAxis1, angleDelta1);
            _grabbable.Transform.RotateAround(PivotTransform.position, rotationAxis2, angleDelta2);

            _previousHandsVectorOnPlane1 = handsVector1;
            _previousHandsVectorOnPlane2 = handsVector2;
        }

        public void EndTransform() { }

        private Vector3 CalculateRotationAxisInWorldSpace(int num)
        {
            Vector3 worldAxis = Vector3.zero;
            if (num == 1) {
                worldAxis[(int)_rotationAxis1] = 1f;
            } else {
                worldAxis[(int)_rotationAxis2] = 1f;
            }
            
            return PivotTransform.TransformDirection(worldAxis);
        }

        private Vector3 CalculateHandsVectorOnPlane(Vector3 planeNormal)
        {
            Vector3[] grabPointsOnPlane =
            {
                Vector3.ProjectOnPlane(_grabbable.GrabPoints[0].position, planeNormal),
                Vector3.ProjectOnPlane(_grabbable.GrabPoints[1].position, planeNormal),
            };

            return grabPointsOnPlane[1] - grabPointsOnPlane[0];
        }

        #region Inject

        public void InjectOptionalPivotTransform(Transform pivotTransform)
        {
            _pivotTransform = pivotTransform;
        }

        public void InjectOptionalRotationAxis(Axis rotationAxis1, Axis rotationAxis2)
        {
            _rotationAxis1 = rotationAxis1;
            _rotationAxis2 = rotationAxis2;
        }

        #endregion
    }
}

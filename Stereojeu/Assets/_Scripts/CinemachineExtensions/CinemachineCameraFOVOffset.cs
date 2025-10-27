using UnityEngine;
using UnityEngine.Serialization;

namespace Unity.Cinemachine
{
    /// <summary>
    /// An add-on module for Cm Camera that adds a final offset to the camera
    /// </summary>
    [AddComponentMenu("Cinemachine/Procedural/Extensions/Cinemachine Camera FOV Offset")]
    [ExecuteAlways]
    [SaveDuringPlay]
    public class CinemachineCameraFOVOffset : CinemachineExtension
    {
        /// <summary>
        /// Offset the camera's position by this much (camera space)
        /// </summary>
        [Tooltip("Offset the camera's FOV by this much")]
        public float Offset = 0;

        /// <summary>
        /// When to apply the offset
        /// </summary>
        [Tooltip("When to apply the offset")]
        [FormerlySerializedAs("m_ApplyAfter")]
        public CinemachineCore.Stage ApplyAfter = CinemachineCore.Stage.Aim;
        
        private void Reset()
        {
            Offset = 0;
            ApplyAfter = CinemachineCore.Stage.Aim;
        }

        /// <summary>
        /// Applies the specified offset to the camera state
        /// </summary>
        /// <param name="vcam">The virtual camera being processed</param>
        /// <param name="stage">The current pipeline stage</param>
        /// <param name="state">The current virtual camera state</param>
        /// <param name="deltaTime">The current applicable deltaTime</param>
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == ApplyAfter)
            {
                state.Lens.FieldOfView += Offset;
            }
        }
    }
}

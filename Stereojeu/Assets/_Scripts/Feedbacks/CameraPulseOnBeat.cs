using System;
using Unity.Cinemachine;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CinemachineCameraFOVOffset))]
public class CameraPulseOnBeat : MonoBehaviour
{
   [Header("Scene References")]
   [SerializeField]
   private FmodCallbacks _callbacks;

   [SerializeField]
   private CinemachineCameraFOVOffset _cameraFOV;
   
   [Header("Parameters")]
   [SerializeField]
   private float _pulseStrength = -2;
   //private float _pulseStrengthOnBar = 20;
   [SerializeField]
   private float _pulseDuration = .25f;
   
   [SerializeField]
   [Range(0,1)]
   private float _pulseTiming = .5f;
   protected void Awake()
   {
      _callbacks?.OnBeat.AddListener(Pulse);
   }

   private void OnValidate()
   {
      if (_callbacks == null)
         _callbacks = FindObjectsByType<FmodCallbacks>(FindObjectsInactive.Exclude,FindObjectsSortMode.None)[0]; if (_callbacks == null)
      
      if(_cameraFOV == null)
         _cameraFOV = FindObjectsByType<CinemachineCameraFOVOffset>(FindObjectsInactive.Exclude,FindObjectsSortMode.None)[0];
   }

   void Pulse()
   {
      Sequence s = DOTween.Sequence();

      s.Append(DOTween.To(
            () => _cameraFOV.Offset,
            (val) => _cameraFOV.Offset = val,
            _pulseStrength,
            _pulseDuration*_pulseTiming)
         .SetEase(Ease.OutSine));
      
      s.Append(DOTween.To(
            () => _cameraFOV.Offset,
            (val) => _cameraFOV.Offset = val,
            0,
            _pulseDuration*(1f-_pulseTiming))
         .SetEase(Ease.InOutSine));
      
   }
}

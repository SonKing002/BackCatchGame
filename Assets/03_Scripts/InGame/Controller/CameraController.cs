using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraController : MonoBehaviour
{
    //투명화 할 레이어의 장애물들
    [SerializeField] private LayerMask _obstacleMask;
    //가리키는 캐릭터의 트랜스폼
    [SerializeField] private Transform _playerTransform;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _playerTransform.position);

    }
    private void FixedUpdate()
    {
        //레이캐스트로 쏴서 얻는 정보를 저장할 배열들
        RaycastHit[] hits;
        //카메라의 거리와 플레이어의 포지션의 거리 차이 계산
        float _distance = Vector3.Distance(transform.position, _playerTransform.position);
        //카메라와 플레이어 간의 각도
        Vector3 _direction = (_playerTransform.position - transform.position).normalized;
        //그 사이에 부딫힌 모든 장애물 레이어를 가진 것들을hits에 저장
        hits = Physics.RaycastAll(transform.position, _direction, _distance, _obstacleMask);
        
        for (int i = 0 ; i < hits.Length; ++i)
        {
            //hits 배열
            RaycastHit hit = hits[i];
            //
            Renderer _obstacleRenderer = hit.transform.GetComponent<Renderer>();
            Debug.Log(_obstacleRenderer == null);

            if (_obstacleRenderer != null)
            {
                // 3. Metrial의 Aplha를 바꾼다.

                Material _material = _obstacleRenderer.material;

                Color _materialColor = _material.color;

                _materialColor.a = 0.5f;

                _material.color = _materialColor;
            }
        }
    }
}
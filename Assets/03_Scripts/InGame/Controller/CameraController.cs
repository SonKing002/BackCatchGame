using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraController : MonoBehaviour
{
    //����ȭ �� ���̾��� ��ֹ���
    [SerializeField] private LayerMask _obstacleMask;
    //����Ű�� ĳ������ Ʈ������
    [SerializeField] private Transform _playerTransform;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _playerTransform.position);

    }
    private void FixedUpdate()
    {
        //����ĳ��Ʈ�� ���� ��� ������ ������ �迭��
        RaycastHit[] hits;
        //ī�޶��� �Ÿ��� �÷��̾��� �������� �Ÿ� ���� ���
        float _distance = Vector3.Distance(transform.position, _playerTransform.position);
        //ī�޶�� �÷��̾� ���� ����
        Vector3 _direction = (_playerTransform.position - transform.position).normalized;
        //�� ���̿� �΋H�� ��� ��ֹ� ���̾ ���� �͵���hits�� ����
        hits = Physics.RaycastAll(transform.position, _direction, _distance, _obstacleMask);
        
        for (int i = 0 ; i < hits.Length; ++i)
        {
            //hits �迭
            RaycastHit hit = hits[i];
            //
            Renderer _obstacleRenderer = hit.transform.GetComponent<Renderer>();
            Debug.Log(_obstacleRenderer == null);

            if (_obstacleRenderer != null)
            {
                // 3. Metrial�� Aplha�� �ٲ۴�.

                Material _material = _obstacleRenderer.material;

                Color _materialColor = _material.color;

                _materialColor.a = 0.5f;

                _material.color = _materialColor;
            }
        }
    }
}
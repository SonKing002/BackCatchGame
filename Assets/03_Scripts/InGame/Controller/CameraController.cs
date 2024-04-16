using System;
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
    private void Update()
    {
        //����ĳ��Ʈ�� ���� ��� ��5���� ������ �迭��
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

            MaterialAlphaChanger materialAlphaChanger = hit.collider.GetComponent<MaterialAlphaChanger>();
            if(materialAlphaChanger == null)
            {
                continue;
                //throw new NotImplementedException();
            }
            materialAlphaChanger.alphaChange = true;

        }
    }
}
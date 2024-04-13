using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushManagement : MonoBehaviour
{
    [SerializeField] private float _viewAngle;
    [SerializeField] private float _viewDistance;
    [SerializeField] private LayerMask targetMask;

    private void Update()
    {
        View();
    }
    public Vector3 GetTargetPos()
    {
        return transform.position;
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        //???
        _angle += -transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }

    public bool View()
    {
        //좌측 경계선
        Vector3 _leftBoundary = BoundaryAngle(-_viewAngle * 0.5f);
        //우측 경계선
        Vector3 _rightBoundary = BoundaryAngle(_viewAngle * 0.5f);

        //직접 좌측 우측 그리기
        Debug.DrawRay(transform.position + transform.up , _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up , _rightBoundary, Color.red);

        Collider[] _target = Physics.OverlapSphere(transform.position, _viewDistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if (_targetTf.name == "Player")
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle < _viewAngle * 0.5f)
                {

                    if (Physics.Raycast(transform.position + transform.up + transform.forward, _direction, out RaycastHit _hit, _viewDistance))
                    {
                        Debug.Log(_hit);
                        if (_hit.transform.name == "Player")
                        {
                            Debug.Log("플레이어가 소 시야내에 있습니다.");
                            Debug.DrawRay(transform.position + transform.up + transform.forward, _direction, Color.blue);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}

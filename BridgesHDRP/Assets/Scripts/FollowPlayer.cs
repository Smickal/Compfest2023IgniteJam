using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform _followSpot;
    [SerializeField] float _maxDistanceToPlayer = 1f;
    [SerializeField] float _followSpeed = 1f;
    [SerializeField] Rigidbody _rigidBody;


    private void LateUpdate()
    {
        float distanceToPlayer = Vector3.Distance(_followSpot.position, transform.position);
        Vector3 direction = _followSpot.position - transform.position;
        //direction.y = 0f;
        direction.Normalize();

        if (distanceToPlayer > _maxDistanceToPlayer)
        {
            //try to move to player
            //_rigidBody.velocity = direction * _followSpeed * Time.deltaTime;
            transform.Translate(direction * _followSpeed * Time.deltaTime);
        }
    }
}

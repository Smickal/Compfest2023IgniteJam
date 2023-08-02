using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform _followSpot;
    [SerializeField] float _maxDistanceToPlayer = 2f;
    [SerializeField] float _followSpeed = 1f;



    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(_followSpot.position, transform.position);
        Vector3 direction = _followSpot.position - transform.position;

        direction.y = 0f;

        if(distanceToPlayer > _maxDistanceToPlayer)
        {
            //try to move to player
            transform.Translate(direction * _followSpeed * Time.deltaTime);

        }
    }

}

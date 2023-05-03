using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ibo
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private List<Transform> rims = new();
        [SerializeField] private GameObject ball;
        [SerializeField] private Transform ballSpawnPoint;
        [SerializeField] private float shootForce;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void RotatePlayer()
        {

        }

        private void Shoot()
        {
            ball.transform.position = ballSpawnPoint.position;


        }
    }
}
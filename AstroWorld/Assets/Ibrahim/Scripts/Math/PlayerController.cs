using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Camera cam;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direction = (hit.point - bulletSpawnPoint.position).normalized;

                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, 100))
        {
            GetComponent<Rigidbody>().AddTorque(transform.right * (Time.fixedDeltaTime * 100));
        }

        if (!Mathf.Approximately(0f, 100))
        {
            GetComponent<Rigidbody>().AddTorque(transform.forward * (Time.fixedDeltaTime * 100));
        }

        if (!Mathf.Approximately(0f, 100))
        {
            GetComponent<Rigidbody>().AddTorque(transform.up * (Time.fixedDeltaTime * 100));
        }
    }
}

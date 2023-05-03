using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            int number = Convert.ToInt32(other.transform.GetComponentInChildren<TextMeshProUGUI>().text);
            GameManager.Instance.AddNumbersToSum(number);
            UIController.Instance.questionNums.Add(number);
            UIController.Instance.UpdateQuestionText();
            
            GameManager.Instance.targetObjects.Remove(other.gameObject);
            Destroy(other.gameObject);
            
            UIController.Instance.UpdateScoreText();
            Destroy(gameObject);
        }
    }
}
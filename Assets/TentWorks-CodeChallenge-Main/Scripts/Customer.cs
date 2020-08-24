﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    private bool isAngry;
    private bool isSatisfied;
    private bool isServed;

    private float timeLeft;
    private float threshold;

    public int existingCustomers;
    public int slotNum;

    private GameObject custTimer;

    public Image customerTimer1;
    public Image customerTimer2;
    public Image customerTimer3;
    public Image customerTimer4;
    public Image customerTimer5;

    Recipe recipe;

    public string[] ingredients;

    void Start()
    {
        isAngry = false;
        isSatisfied = false;
        recipe = new Recipe();
        
        timeLeft = Random.Range(40, 80);
        ingredients = recipe.GetRecipe();
        threshold = timeLeft * .7f;

        AssignTimer();
    }

    public void AssignTimer()
    {
        Timer t = new Timer();
        string rString = "";

        for(int i = 0; i < ingredients.Length; i++)
        {
            string s = ingredients[i];
            if(i == ingredients.Length - 1)
            {
                rString += s;
            }
            else
            {
                rString += s + ", ";
            }
        }

        GameObject timerCanvas = GameObject.FindGameObjectWithTag("Timer");

        switch (slotNum)
        {
            case 0:
                gameObject.name = "Customer 1";
                customerTimer1 = timerCanvas.transform.Find("CustomerTimer1/Image").GetComponent<Image>();
                t = customerTimer1.GetComponent<Timer>();
                t.timeLeft = timeLeft;
                t.reset = true;
                t.recipeText.text = rString;
                break;
            case 1:
                gameObject.name = "Customer 2";
                customerTimer2 = timerCanvas.transform.Find("CustomerTimer2/Image").GetComponent<Image>();
                t = customerTimer2.GetComponent<Timer>();
                t.timeLeft = timeLeft;
                t.reset = true;
                t.recipeText.text = rString;
                break;
            case 2:
                gameObject.name = "Customer 3";
                customerTimer3 = timerCanvas.transform.Find("CustomerTimer3/Image").GetComponent<Image>();
                t = customerTimer3.GetComponent<Timer>();
                t.timeLeft = timeLeft;
                t.reset = true;
                t.recipeText.text = rString;
                break;
            case 3:
                gameObject.name = "Customer 4";
                customerTimer4 = timerCanvas.transform.Find("CustomerTimer4/Image").GetComponent<Image>();
                t = customerTimer4.GetComponent<Timer>();
                t.timeLeft = timeLeft;
                t.reset = true;
                t.recipeText.text = rString;
                break;
            case 4:
                gameObject.name = "Customer 5";
                customerTimer5 = timerCanvas.transform.Find("CustomerTimer5/Image").GetComponent<Image>();
                t = customerTimer5.GetComponent<Timer>();
                t.timeLeft = timeLeft;
                t.reset = true;
                t.recipeText.text = rString;
                break;
        }
    }
}

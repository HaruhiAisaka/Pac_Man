using System;
using System.Collections.Generic;
using UnityEngine;

public class NumberDisplay : MonoBehaviour
{
    public Sprite[] numberSprite = new Sprite[10];
    public SpriteRenderer[] numberTiles;

    private Stack<int> listOfDigits = new Stack<int>();

    public int number;

    void Start()
    {
        foreach (SpriteRenderer numberTile in numberTiles)
        {
            numberTile.enabled = false;
        }
        UpdateNumber(number);
    }

    public void UpdateNumber(int number)
    {
        if (number >= Mathf.Pow(10, numberTiles.Length))
        {
            throw new ArgumentException("Number must have less digits than " + numberTiles.Length);
        }
        this.number = number;
        ToListOfDigits(number);
        int count = listOfDigits.Count;
        for (int i = 0; i < count; i++)
        {
            int digit = listOfDigits.Pop();
            numberTiles[i].enabled = true;
            numberTiles[i].sprite = numberSprite[digit];
        }
    }
    
    protected void ToListOfDigits(int number)
    {
        listOfDigits.Clear();
        if (number == 0)
        {
            listOfDigits.Push(0);
        }
        else if (number < 0)
        {
            throw new ArgumentException("Number can not be negative");
        }
        else
        {
            ToListOfDigitsRec(number);
        }
    }


    protected void ToListOfDigitsRec(int number)
    {
        if (number == 0) return;

        ToListOfDigitsRec(number / 10);
        listOfDigits.Push(number % 10);
    }

    protected Sprite SpriteFormDigit(int digit)
    {
        if (digit >= 10)
        {
            throw new ArgumentException("Number must be less than 10");
        }
        return numberSprite[digit];
    }
}

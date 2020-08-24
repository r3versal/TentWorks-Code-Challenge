using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegObjectType : MonoBehaviour
{
    private string[] m_HardCodedStrings;

    void Awake()
    {
        m_HardCodedStrings = new string[]
            {
                 "Lettuce",
                 "Carrot",
                 "Onion",
                 "RedBellPepper",
                 "YellowBellPepper",
                 "Tomato"
            };
    }

    public string[] GetVegetables()
    {
        return m_HardCodedStrings;
    }
}

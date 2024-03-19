using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetValueDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    private string selectedOption = "1";

    public void GetDropdownValue()
    {
        int pickedEntryIndex = dropdown.value;
        selectedOption = dropdown.options[pickedEntryIndex].text;
        Debug.Log(selectedOption);
    }
    public string GetSelectedOption()
    {
        return selectedOption;
    }
}

using UnityEngine;
using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour // use this to referance the TMP_Text and GameObjects needed for the functions
{
    public GameObject screen; // the base rectangle, the size created in the project menu
    public TMP_Text text; 
}

public class CRT_SCREEN_MENU
{
    public List<GUID> menuChioces;
    public int index = 0;
    public void MakeMenu(Menu menu)
    {
        if(index > menu.children.Count) { index = menu.children.Count; }
        if(index < 0) { index = 0; }
        for (int i = 0; i <  menu.children.Count; i++)
        {
            string text = "";
            if(i == index && menu.children[i].selectable == true)
            {
                text = "> " + menu.children[i].text;
            }
            else if (i == index && menu.children[i].selectable == false)
            {
                index += 1;
                text = menu.children[i].text;
            }
            else
            {
                text = menu.children[i].text;
            }
            // make the text be visible on screen
        }
    }

    public int MenuSelect(Menu menu)
    {
        return menu.children[index].id; // API ENDPOINT: returns the id of the selected item so one can get the id, look it up, and know what to do with the product
    }

    public void menuUp()
    {
        index--;
    }

    public void menuDown()
    {

        index++;
    }

    public void changeMenuItem(Menu menu, GUID item, string change)
    {
        MenuObject newMO = null;
        int index = 0;
        for(int i = 0; i < menu.children.Count; i++)
        {
            if(menu.children[i].id == item)
            {
                newMO = menu.children[i];
                index = i;
                break;
            }
        }
        newMO.text = change;
        menu.children[index] = newMO;
    }
}
public class MenuObject
{
    public string text;
    public bool selectable;
    public GUID id;
    public MenuObject parent; // indent it and make it non selectable unless parent is selected
}
public class Menu
{
    public string Title;
    public List<MenuObject> children;
    public GUID id;
}
public class CRT_SCREEN_TEXT // text creator
{
    //
    public void CREATE_TEXT()
    {
        // create a TMP_Text element for the title and screen
        // set the font to be the project font
        // set the size of it 
    }

    public void WRITE_TEXT(string text) // seperate the text new lines by "\n"
    {
        string[] finishedText = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);

    }
}

public class CRT_SCREEN_GRAPHICS // 2d vector graphics
{
    //
}



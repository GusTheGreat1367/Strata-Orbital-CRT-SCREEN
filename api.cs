using UnityEngine;
using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour // use this to referance the TMP_Text and GameObjects needed for the functions
{
    public GameObject screen; // the base rectangle, the size created in the project menu
    public TMP_Text text; 
}

public class CRT_SCREEN_MENU
{
    public List<Guid> menuChioces;
    public int index = 0;
    bool selected = false; // if an option has been selected or not
    bool flipped = false; // if the selectability of the SelectableObjects have been flipped
    public void MakeMenu(Menu menu)
    {
        // clear the text
        if(index > menu.children.Count) { index = 0; }
        if(index < 0) { index = menu.children.Count-1; }
        for (int i = 0; i <  menu.children.Count; i++)
        {
            string text = "";
            if(i == index && menu.children[i].selectable == true)
            {
                text += "> " + menu.children[i].text +"\n";
            }
            else if (i == index && menu.children[i].selectable == false)
            {
                index += 1;
                text += menu.children[i].text + "\n";
            }
            else
            {
                text += menu.children[i].text + "\n";
            }
            // make the text be visible on screen
        }
    }

    public void MenuSelect(Menu menu)
    {
        if(selected == false)
        {
            if(menu.children[index].children == false)
            {
                string retGuid = menu.children[index].id.ToString();
                // return retGuid; // API ENDPOINT: returns the id of the selected item so one can get the id, look it up, and know what to do with the product
                selected = true;
            }
            else
            {
                flip(menu); // go to the inner list of MenuObjects
            }
        }
    }
    public void createSubMenu(List<string> subs, Menu menu, MenuObject parent)
    {
        //Function to make sub-menus -> create a list<string> make a MenuObject
        // for each of the strings and make the parent be the parent MenuObject then when writing each menuobject indent the sub-menus
        if(menu.children.Contains(parent))
        {
            foreach(var child in subs)
            {
                MenuObject sub = new MenuObject();
                sub.text = child;
                sub.selectable = false;
                sub.id = Guid.NewGuid();
                sub.parent = parent;
                sub.children = false;
                menu.children.Add(sub); // make a new child object who's parent = parent and add it to the list of menu items
            }
        }
    }
    public void MenuGoBack(Menu menu)
    {
        if(selected) { selected = false; }
        if(flipped) { flip(menu); }
        MakeMenu(menu);
    }
    void flip(Menu menu)
    {
        foreach(var child in menu.children)
        {
            child.selectable = !child.selectable;
            flipped = !flipped;
        }
    }
    public void menuUp()
    {
        index--;
    }

    public void menuDown()
    {

        index++;
    }

    public void changeMenuItem(Menu menu, Guid item, string change)
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
    public Guid id;
    public MenuObject parent; // indent it and make it non selectable unless parent is selected
    public bool children;
}
public class Menu
{
    public string Title;
    public List<MenuObject> children;
    public Guid id;
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



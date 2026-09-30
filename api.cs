using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;


namespace CRT_SCREEN
{
    public class CRT_SCREEN_MENU
    {
        public List<Guid> menuChioces;
        public int index = 0;
        bool selected = false; // if an option has been selected or not
        bool flipped = false; // if the selectability of the SelectableObjects have been flipped
        public TMP_Text SCtext;
        public void MakeMenu(Menu menu) 
        {
            int nonSelect = 0;
            selected = false; // reset the selection state when making a new menu
            Debug.Log("Making menu with " + menu.children.Count + " children.");
            foreach(var sel in menu.children)
            {
                Debug.Log("Menu child: " + sel.text);
                if(!sel.selectable)
                {
                    nonSelect++;
                }
            }
            // the "(1+nonSelect)" makes it so you only navigate through the selectable items
            if(!flipped)
            {
                if(index > menu.children.Count-(1+nonSelect)) { index = 0; }
                if(index < 0) { index = menu.children.Count-(1+nonSelect); }
            }
            else // you are in a sub-menu
            {
                if(index > menu.children.Count-1) { index = 0; }
                if(index < 0) { index = menu.children.Count-1; }
            }
            // cycle through the list, so if ur at the bottom of it and go down, go to the top, and vice versa

            // clear the text
            string text = "";
            for (int i = 0; i <=  menu.children.Count-1; i++) 
            {
                if(menu.children[i].selectable == true)
                {
                    if(i == index && menu.children[i].selectable == true) // you can select this item
                    {
                        text += "> " + menu.children[i].text +"\n"; // if it's normaly not selectable do the indentation then the ">"
                    }
                    else 
                    {
                        text += menu.children[i].text + "\n";
                    }
                }
                else if (i == index && menu.children[i].selectable == false) // you can't select this item, skip it
                {
                    index += 1;
                    //text += menu.children[i].text + "\n";
                }
                /*
                else // indent the non-selectable items
                {
                    text += menu.children[i].text + "\n";
                }
                */
                // make the text be visible on screen
                SCtext.text = text;
            }
        }

        /*
        public void MenuSelect(Menu menu) // also bugged
        {
            if(selected == false) // if nothing was selected
            {
                // possibly move this to an "&&" for the true if value?
                Debug.Log("No item selected yet.");
                if(menu.children[index].children == false) // if the current item doesn't have children, mark it as selected
                {
                    selected = true;
                    Debug.Log("Item selected: " + menu.children[index].text);
                }
                else // let's see the item's children
                {
                    Debug.Log("Navigating to children of: " + menu.children[index].text);
                    flip(menu); // go to the inner list of MenuObjects
                }
            }
        }
        */

        public void MenuSelect(Menu menu)
        {
            if(!menu.children[index].children && menu.children[index].selectable && selected == false)
            {
                selected = true;
                Debug.Log("Item selected: " + menu.children[index].text);
            }
            else
            {
                flip(menu); // go to the inner list of MenuObjects
            }
        }

        // FOR THE BUGS, Debug.Log(); until they confess

        public void createSubMenu(List<string> subs, Menu menu, MenuObject parent) 
        {
            parent.children = true;
            //Function to make sub-menus -> create a list<string> make a MenuObject
            // for each of the strings and make the parent be the parent MenuObject then when writing each menuobject indent the sub-menus
            if(menu.children.Contains(parent))
            {
                foreach(var child in subs)
                {
                    MenuObject sub = new MenuObject();
                    //sub.text = "    " + child; <- for deprecated menu navigation
                    sub.text = child;
                    sub.selectable = false;
                    sub.id = Guid.NewGuid();
                    sub.parent = parent;
                    sub.child = true; // it's a child 
                    sub.children = false; // it doesn't have children
                    menu.children.Add(sub); // make a new child object who's parent = parent and add it to the list of menu items
                }
            }
        }
        public Guid GetReturnedValue(Menu menu) // bugged, just everything is bugged,  
        {                                                         //               _  \ /  _
            if(selected)                                          //                \ (0) /
            {                                                     //                 (|#|)
                Guid retGuid = menu.children[index].id;           //               _/ (0) \_
                Debug.Log("Returned value for selected item: " + retGuid);  
                return retGuid; // API ENDPOINT: returns the id of the selected item so one can get the id, look it up, and know what to do with the product
            }
            else
            {
                Debug.Log("No item selected, returning Guid.Empty");
                MakeMenu(menu);
                return Guid.Empty;
            }
        }
        public void MenuGoBack(Menu menu)
        {
            if(selected) { selected = false; }
            if(flipped) { flip(menu); }
            index = 0;
            MakeMenu(menu);
            Debug.Log("Menu went back, index reset to 0");
        }
        void flip(Menu menu)
        {
            foreach(var child in menu.children)
            {
                child.selectable = !child.selectable;
                flipped = !flipped;
            }
            MakeMenu(menu);
        }
        public void menuUp(Menu menu)
        {
            index--;
            MakeMenu(menu);
        }

        public void menuDown(Menu menu)
        {

            index++;
            MakeMenu(menu);
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
            MakeMenu(menu);
        }
    }
    public class MenuObject
    {
        public string text;
        public bool selectable;
        public Guid id;
        public MenuObject parent; // indent it and make it non selectable unless parent is selected
        public bool child; // is it the child of another MenuObject
        public bool children; // does it have any children
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
            // create a TMP_Text for the screen
            // set the font to be the project font
            // set the size of it 
        }

        public void WRITE_TEXT(string text) // seperate the text new lines by "\n"
        {
            //string[] finishedText = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        }
    }

    public class CRT_SCREEN_GRAPHICS // 2d vector graphics
    {
        //
    }
}

public class functions
{
    public GameObject Instantiate(GameObject obj, Vector2 pos, Quaternion rot)
    {
        GameObject new_obj = obj;
        new_obj.transform.position = pos;
        new_obj.transform.rotation = rot;
        return new_obj;
    }
}

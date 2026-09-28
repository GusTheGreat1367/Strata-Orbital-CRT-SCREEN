using UnityEngine;
using CRT_SCREEN; // the library needed
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
public class CRT_SCREEN_TEST : MonoBehaviour
{
    // THE INPUT SYSTEM FOR THIS EXAMPLE/TEST USAGE IS MenuNav, with an input Up and Down
    [Header("Text")]
    public TMP_Text text;

    public InputActions player;
    public InputAction select;
    public InputAction up;
    public InputAction down;
    public InputAction yes;
    public InputAction no;
    Menu menu = new Menu()
    {
        Title = "Menu 01",
        id = Guid.NewGuid(),
        children = new List<MenuObject>()
    };
    MenuObject one = new MenuObject()
    {
        text = "Start",
        selectable = true,
        id = Guid.NewGuid(),
        parent = new MenuObject(), 
        child = false,
        children = false  
    };
    MenuObject three = new MenuObject()
    {
        text = "Settings",
        selectable = true,
        id = Guid.NewGuid(),
        parent = new MenuObject(), 
        child = false,
        children = false  
    };
    CRT_SCREEN_MENU crt_screen_menu = new CRT_SCREEN_MENU();
    void Awake()
    {
        player = new InputActions();
        up = player.menuNav.Up;
        down = player.menuNav.Down;
        yes = player.menuNav.Right;
        no = player.menuNav.Left;
        select = player.menuNav.change;
    }
    public void OnEnable()
    {
        up.Enable();
        down.Enable();
        yes.Enable();
        no.Enable();
        select.Enable();
    }
    public void OnDisable()
    {
        up.Disable();
        down.Disable();
        no.Disable();
        yes.Disable();
        select.Disable();
    }
    void Start()
    {
        crt_screen_menu.SCtext = text;
        menu.children.Add(one);
        menu.children.Add(three);
        List<string> subs = new List<string>()
        {
            "Basic stuff",
            "Return"
        };
        crt_screen_menu.createSubMenu(subs, menu, three);
        crt_screen_menu.MakeMenu(menu);
    }

    // Update is called once per frame
    void Update()
    {
        if(up.WasPressedThisFrame()) // navigate up on the menu
        {
            crt_screen_menu.menuUp(menu);
        }
        if(down.WasPressedThisFrame()) // navigate down on the menu
        {
            crt_screen_menu.menuDown(menu);
        }
        if(yes.WasPressedThisFrame()) // select the current value
        {
            crt_screen_menu.MenuSelect(menu);
            Guid selected_object = crt_screen_menu.GetReturnedValue(menu); 
            // BEWARE!!! If an object wasn't selected, and the menu just moved to a submenu, the selected_object will be a Guid.Empty
            /* do this to check:
            if(selected_object != Guid.Empty) { runCode(); }
            */
            Debug.Log(selected_object);
        }
        if(no.WasPressedThisFrame()) // revert back on the menu
        {
            crt_screen_menu.MenuGoBack(menu);
        }
        if(select.WasPressedThisFrame()) //HOW TO CHANGE JUST THE ITEM "one" IN THE MENU TO "1"
        {
            crt_screen_menu.changeMenuItem(menu, one.id, "1");
        }
    }
}

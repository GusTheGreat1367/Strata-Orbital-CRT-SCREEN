using UnityEngine;
using CRT_SCREEN; // the library needed
using System;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CRT_SCREEN_TEST : MonoBehaviour
{
    // THE INPUT SYSTEM FOR THIS EXAMPLE/TEST USAGE IS MenuNav, with an input Up and Down
    [Header("Text")]
    public TMP_Text text;

    public InputActions player;
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
        text = "One",
        selectable = true,
        id = Guid.NewGuid(),
        parent = new MenuObject(), 
        child = false  
    };
    MenuObject two = new MenuObject()
    {
        text = "Two",
        selectable = true,
        id = Guid.NewGuid(),
        parent = new MenuObject(), 
        child = false  
    };
    MenuObject three = new MenuObject()
    {
        text = "Three",
        selectable = true,
        id = Guid.NewGuid(),
        parent = new MenuObject(), 
        child = true  
    };
    CRT_SCREEN_MENU crt_screen_menu = new CRT_SCREEN_MENU();
    void Awake()
    {
        player = new InputActions();
        up = player.menuNav.Up;
        down = player.menuNav.Down;
        yes = player.menuNav.Right;
        no = player.menuNav.Left;
    }
    public void OnEnable()
    {
        up.Enable();
        down.Enable();
        yes.Enable();
        no.Enable();
    }
    public void OnDisable()
    {
        up.Disable();
        down.Disable();
        no.Disable();
        yes.Disable();
    }
    void Start()
    {
        crt_screen_menu.SCtext = text; // init the TMP_Text element and set it to the normal text
        // populate the menu array of children
        menu.children.Add(one);
        menu.children.Add(two);
        menu.children.Add(three);
        List<string> subs = new List<string>()
        {
            "Sub-one",
            "Sub-2"
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
        }
        if(no.WasPressedThisFrame()) // revert back on the menu
        {
            crt_screen_menu.MenuGoBack(menu);
        }
        //TEST: HOW TO CHANGE JUST THE ITEM "one" IN THE MENU TO "1"
        //crt_screen_menu.changeMenuItem(menu, one.id, "1");
    }

}
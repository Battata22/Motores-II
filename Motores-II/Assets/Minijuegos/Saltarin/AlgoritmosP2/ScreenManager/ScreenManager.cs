using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    Stack<IScreen> _screens = new Stack<IScreen>();

    public IScreen CanvasVictory;
    public IScreen CanvasDerrota;

    private void Awake()
    {
        instance = this;
    }

    public bool IsPausedActive { get { return _screens.Count > 0; } }

    public void ActiveScreen(IScreen screen)
    {
        if (_screens.Count > 0)
        {
            //Peek consigue la ultima de la lista pero no la borra
            _screens.Peek().Deactivate();
        }

        screen.Activate();

        //Push es lo mismo que un .Add en una lista
        _screens.Push(screen);
    }

    public void DesactiveScreen()
    {
        if (_screens.Count <= 0) return;

        //Pop te devuelve el ultimo que se añadio (LIFO)
        _screens.Pop().Deactivate();

        if (_screens.Count <= 0) return;

        _screens.Peek().Activate();
    }
}

public interface IScreen
{
    void Activate();
    void Deactivate();
}

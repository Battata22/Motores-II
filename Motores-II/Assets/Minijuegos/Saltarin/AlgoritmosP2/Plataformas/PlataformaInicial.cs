using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaInicial : Rewind
{
    [SerializeField] GameObject _tutorialImagen;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public override void Save()
    {
        mementoState.Rec(transform.position, gameObject.activeInHierarchy, _tutorialImagen.activeInHierarchy);
    }

    public override void Load()
    {
        if (!mementoState.IsRemember()) return;

        var remember = mementoState.Remember();

        transform.position = (Vector3)remember.parameters[0];
        gameObject.SetActive((bool)remember.parameters[1]);
        _tutorialImagen.SetActive((bool)remember.parameters[2]);
    }

    public override void RemoveMe()
    {

    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaivour>() != null)
        {
            _tutorialImagen.SetActive(false);
            SaltarinManager.instance.ActivarTrigger();
            gameObject.SetActive(false);
        }
    }


}

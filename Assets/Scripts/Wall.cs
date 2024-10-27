using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Subject _player; 
    private void OnTriggerEnter(Collider other)
    {
        if(ReferenceEquals(other, _player.GetComponent<Collider>()))
        {
            switch (tag)
            {
                case "wall1":
                    _player.NotifyObservers(PlayerActions.Phone1Reached);
                    return;
                case "NPCWall":
                    _player.GetComponent<PlayerMovement>().raycast = true;
                    gameObject.SetActive(false);
                    return;
                case "wall3":
                    _player.NotifyObservers(PlayerActions.Phone2Reached);
                    return;
            }
        }
    }
}
